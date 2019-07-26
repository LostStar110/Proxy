using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MKProxyGatewayServer
{
    public class ReverseProxyMiddleware
    {
        //请求传递
        private static readonly HttpClient _httpClient = new HttpClient();

        private readonly RequestDelegate _nextMiddleware;
        private readonly IConfiguration _configuration;

        public ReverseProxyMiddleware(RequestDelegate nextMiddleware, IConfiguration configuration)
        {
            _nextMiddleware = nextMiddleware;
            _configuration = configuration;

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));


        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/api/WSConfig"))
            {
                await _nextMiddleware(context);
                return;
            }

            var targetUri = BuildTargetUri(context.Request);
          


            if (targetUri==null)
            {
                //若url获取为空(服务器配置出错)，拦截所有请求
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("主从服务器配置出错");
                return;
            }
            else
            {
             
                var targetRequestMessage = CreateTargetMessage(context, targetUri);

                using (var responseMessage = await _httpClient.SendAsync(targetRequestMessage, HttpCompletionOption.ResponseHeadersRead, context.RequestAborted))
                {
                    context.Response.StatusCode = (int)responseMessage.StatusCode;
                    CopyFromTargetResponseHeaders(context, responseMessage);
                    await responseMessage.Content.CopyToAsync(context.Response.Body);
                }
                return;
            }
            //await _nextMiddleware(context);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="targetUri"></param>
        /// <returns></returns>
        private HttpRequestMessage CreateTargetMessage(HttpContext context, Uri targetUri)
        {
            var requestMessage = new HttpRequestMessage();

            CopyFromOriginalRequestContentAndHeaders(context, requestMessage);

            requestMessage.RequestUri = targetUri;
            requestMessage.Headers.Host = targetUri.Host;

            requestMessage.Method = GetMethod(context.Request.Method);

            return requestMessage;
        }


        private async void  CopyFromOriginalRequestContentAndHeaders(HttpContext context, HttpRequestMessage requestMessage)
        {
            var requestMethod = context.Request.Method;

            if (!HttpMethods.IsGet(requestMethod) &&
              !HttpMethods.IsHead(requestMethod) &&
              !HttpMethods.IsDelete(requestMethod) &&
              !HttpMethods.IsTrace(requestMethod))
            {
                var streamContent = new StreamContent(context.Request.Body);
                if (!string.IsNullOrEmpty(context.Request.ContentType))
                {
                    streamContent.Headers.Add("Content-Type", context.Request.ContentType);
                }

                requestMessage.Content = streamContent;

                #region 因为请求数据类型不对导致访问失败的尝试
                //if (context.Request.ContentType== "application/json;charset=UTF-8")
                //{
                //    var reader = new StreamReader(context.Request.Body);
                //    var content = await reader.ReadToEndAsync();
                //    requestMessage.Content = new StringContent(content, Encoding.UTF8, "application/json");
                //}
                //else if(context.Request.ContentType.Contains("multipart/form-data;"))
                //{

                //    //var stream = context.Request.Body;


                //    //byte[] buffer = new byte[(int)context.Request.ContentLength];
                //    //stream.Read(buffer, 0, buffer.Length);

                //    //var formContent = new MultipartFormDataContent();

                //    //formContent.Add(new ByteArrayContent(buffer));

                //    //using (var fs =new FileStream("test.xlsx",FileMode.OpenOrCreate))
                //    //{
                //    //     await fs.WriteAsync(buffer);

                //    //}

                //    var formContent = new MultipartFormDataContent();
                //    formContent.Add(new StreamContent(context.Request.Body));

                //    formContent.Headers

                //    requestMessage.Content = formContent;

                //}
                #endregion

            }

            foreach (var header in context.Request.Headers)
            {
                requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
            }
        }

        private void CopyFromTargetResponseHeaders(HttpContext context, HttpResponseMessage responseMessage)
        {
            foreach (var header in responseMessage.Headers)
            {
                context.Response.Headers[header.Key] = header.Value.ToArray();
            }

            foreach (var header in responseMessage.Content.Headers)
            {
                context.Response.Headers[header.Key] = header.Value.ToArray();
            }
            context.Response.Headers.Remove("transfer-encoding");
        }

        private static HttpMethod GetMethod(string method)
        {
            if (HttpMethods.IsDelete(method)) return HttpMethod.Delete;
            if (HttpMethods.IsGet(method)) return HttpMethod.Get;
            if (HttpMethods.IsHead(method)) return HttpMethod.Head;
            if (HttpMethods.IsOptions(method)) return HttpMethod.Options;
            if (HttpMethods.IsPost(method)) return HttpMethod.Post;
            if (HttpMethods.IsPut(method)) return HttpMethod.Put;
            if (HttpMethods.IsTrace(method)) return HttpMethod.Trace;
            return new HttpMethod(method);
        }

        /// <summary>
        /// 替换为代理服务器的url
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private Uri BuildTargetUri(HttpRequest request)
        {
            Uri targetUri = null;

            var mainUrl = MonitoringHealth.MainUrl;
            var bakeUrl = MonitoringHealth.BakeUrl;

            if (string.IsNullOrEmpty(mainUrl)||string.IsNullOrEmpty(bakeUrl))
            {
                return null;
            }

            if (MonitoringHealth.MainServerHealth)
            {
                targetUri = new Uri(mainUrl + request.Path);
            }
            else if (MonitoringHealth.BakeServerHealth)
            {
                targetUri = new Uri(bakeUrl + request.Path);
            }
            else
            {
                return null;
            }

            return targetUri;
        }



    }
}
