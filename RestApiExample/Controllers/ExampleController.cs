using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Routing;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using RestApiExample.Handlers;
namespace RestApiExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class ExampleController : ControllerBase
    {
        #region Fields

        private readonly ILogger<ExampleController> _logger;
        private CancellationTokenSource? _cancellationTokenSource;

        #endregion

        #region Constructors

        public ExampleController(ILogger<ExampleController> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Endpoints/Resources ASYNC 

        [HttpGet("action-passing-cancellation-async")]
        public async Task<string> GetActionPassingCancellationAsync(string? inputText, CancellationToken cancellationToken)
        {
            //NET8 and using 'GlobalExceptionHandler : IExceptionHandler'
            //When the client cancel the operation the server automatically generates an OperationCanceledException. It's OK,
            //But in my tests the Exception OperationCanceledException is not sent to 'GlobalExceptionHandler.TryHandleAsync(...)'
            //So i had to wrap the entire method with try catch to replace OperationCanceledException with Exception (REF:001)

            try
            {
                _logger.LogInformation($"Server Action-Passing-Cancellation-Async Start, Text={inputText}");

                string result = await ActionWithCancellationAsync(inputText, cancellationToken);

                _logger.LogInformation($"Server Action-Passing-Cancellation-Async completed, Text={inputText}");
                return result;
            }
            catch (OperationCanceledException ex1)
            {
                _logger.LogInformation($"OperationCanceledException:{ex1.Message}");
                throw new Exception("Server Action cancelled1."); //REF:001
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Server Action-Passing-Cancellation-Async, Exception, Error:{ex.Message}");
                throw;
            }
        }

        [HttpGet("action-internal-cancellation-async")]

        public async Task<string> GetActionInternalCancellationAsync( string inputText) //[FromQuery]
        {
            _logger.LogInformation($"Server Action-internal-Cancellation-Async Start, Text={inputText}");

            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationTokenSource.CancelAfter(15000);
            var cancellationToken = _cancellationTokenSource.Token;

            var result = await ActionWithCancellationAsync(inputText, cancellationToken);

            _logger.LogInformation($"Server Action-Passing-Cancellation-Async completed, Text={inputText}");
            return result;
        }

        //example with try catch 
        [HttpGet("action-internal-cancellation2-async")]
        public async Task<string> GetActionInternalCancellation2Async(string inputText) //[FromQuery]
        {
            try
            {
                _logger.LogInformation($"Server Action-internal-Cancellation-Async Start, Text={inputText}");

                _cancellationTokenSource = new CancellationTokenSource();
                _cancellationTokenSource.CancelAfter(15000);
                var cancellationToken = _cancellationTokenSource.Token;

                var result = await ActionWithCancellationAsync(inputText, cancellationToken);

                _logger.LogInformation($"Server Action-Passing-Cancellation-Async completed, Text={inputText}");

                return result;
            }
            catch (OperationCanceledException ex1)
            {
                _logger.LogInformation($"OperationCanceledException:{ex1.Message}");
                throw new Exception("Server Action cancelled1."); //REF:001
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exception:{ex.Message}");
                throw;
            }
        }

        //demonstration only
        internal void Cancel() 
        {
            _cancellationTokenSource?.Cancel();
        }

        #endregion

        #region Private Methods ASYNC

        private async Task<string> ActionWithCancellationAsync(string? inputText, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Server Action Start");

            for (int i = 0; i < 50; i++)
            {
                cancellationToken.ThrowIfCancellationRequested(); //cancellation

                _logger.LogInformation($"Server Action loop: [{i}]");

                await Task.Delay(1000, cancellationToken); //cancellation
            }

            _logger.LogInformation($"Server Action Finished, Text:{inputText}");

            return $"Server Action Finished, Text={inputText}";
        }

        #endregion
    }
}

