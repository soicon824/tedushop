using Shop.Model.Models;
using Shop.Service;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Net.Http;
using System.Web.Http;

namespace Shop.Web.Infrastructure.Core
{
    public class ApiControllerBase : ApiController
    {
        private IErrorService _errorService;
        public ApiControllerBase(IErrorService errorService)
        {
            this._errorService = errorService;
        }

        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage requestMessage,
            Func<HttpResponseMessage> func
            )
        {
            HttpResponseMessage response = null;
            try
            {
                return func.Invoke();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var eve in dbEx.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state\"{eve.Entry.State}\" has error");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"Property \"{ve.PropertyName}\" error\"{ve.ErrorMessage}\"");
                    }
                }
                LogError(dbEx);
                response = requestMessage.CreateResponse(System.Net.HttpStatusCode.BadRequest,
                    dbEx.InnerException.Message);
            }
            catch (DbUpdateException dbEx)
            {
                LogError(dbEx);
                response = requestMessage.CreateResponse(System.Net.HttpStatusCode.BadRequest,
                    dbEx.InnerException.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                response = requestMessage.CreateResponse(System.Net.HttpStatusCode.BadRequest,
                    ex.Message);
            }
            return response;
        }

        private void LogError(Exception ex)
        {
            try
            {
                Error error = new Error();
                error.CreatedDate = DateTime.Now;
                error.Message = ex.Message;
                error.StackTrace = ex.StackTrace;
                _errorService.Create(error);
                _errorService.save();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}