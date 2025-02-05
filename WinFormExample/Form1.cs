using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;


namespace WinFormExample
{
    public partial class Form1 : Form
    {
        #region Fields

        private CancellationTokenSource? _cancellationTokenSource;
        private readonly int _cancellationTokenAfter = 25000;
        private readonly string _apiUrl = $"https://localhost:7223";

        #endregion

        #region Constructors

        public Form1(string apiUrl)
        {
            _apiUrl = apiUrl;

            InitializeComponent();

            Text = $"Url server is':{apiUrl}'";
        }

        #endregion

        #region UI Events

        private void Form1_Load(object sender, EventArgs e)
        {
            ChangeFormStatus(true);
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            textBoxParam.Focus();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                CancelCancellationToken();
            }
            catch
            {
            }
        }

        private async void ButtonRun_Click(object sender, EventArgs e)
        {
            //bool isCancelled = false;

            try
            {
                ChangeFormStatus(false);

                _cancellationTokenSource = new CancellationTokenSource();
                _cancellationTokenSource.CancelAfter(_cancellationTokenAfter);
                var cancellationToken = _cancellationTokenSource.Token;

                //await Task.Delay(10000, cancellationToken);
                var result = await ActionRunsync(textBoxParam.Text, cancellationToken);

                textBoxResponse.Text = result;
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case TaskCanceledException:
                        //isCancelled = true;
                        break;
                    default:
                        break;
                }

                MessageError(ex);
            }
            finally
            {
                _cancellationTokenSource?.Dispose();
                ChangeFormStatus(true);
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            CancelCancellationToken();
            ChangeFormStatus(true);
        }

        #endregion

        #region private Main Methods

        private async Task<string> ActionRunsync(string text, CancellationToken cancellationToken)
        {
            string url = $"{_apiUrl}/Example/action-passing-cancellation-async";

            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (textBoxParam.Text.Trim() != string.Empty)
                query["InputText"] = text;

            uriBuilder.Query = query.ToString();
            var uri = uriBuilder.ToString();

            ///////////////////////

            var httpResponseInfo = await CallRestApiGetAsync(uri, cancellationToken);


            //test other statusCodes and throw
            // if (result.StatusCode == HttpStatusCode.OK)

            //Ok
            //GetArtistsByNameResponse? response = JsonSerializer.Deserialize<GetArtistsByNameResponse>(httpResponseInfo.ContentData, Global.DefaultJsonSerializerOptions)
            //    ?? throw new Exception("'GetArtistsByNameResponse' is null");

            //SearchArtistsResult result = response.Result
            //    ?? throw new Exception("'GetArtistsByNameResponse.Result' is null");

            return httpResponseInfo;
        }

        private static async Task<string> CallRestApiGetAsync(string uri, CancellationToken cancellationToken)
        {
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //execute
            HttpResponseMessage response = await client.GetAsync(uri, cancellationToken); //Note: important passing token

            //get body
            string contentData = await response.Content.ReadAsStringAsync((cancellationToken)); //Note: important passing token

            return contentData;
        }

        #endregion

        #region Private Adjustment Methods

        private void MessageError(Exception ex)
        {
            ChangeFormStatus(false);

            textBoxResponse.Text = ex switch
            {
                Exception => $"{ex.Message}",
                _ => $"{ex.Message}"
            };
        }

        private void ChangeFormStatus(bool enabled)
        {
            //cursor
            if (enabled)
            {
                Cursor = Cursors.Default;
                buttonCancel.Cursor = Cursors.Default;
            }
            else
            {
                Cursor = Cursors.WaitCursor;
                buttonCancel.Cursor = Cursors.AppStarting;
                textBoxResponse.Text = "";
            }

            //enable
            textBoxParam.Enabled = enabled;
            buttonRun.Enabled = enabled;
            buttonCancel.Enabled = !enabled;

            progressBar1.Visible = !enabled;
            progressBar1.Style = enabled ? ProgressBarStyle.Continuous : ProgressBarStyle.Marquee;
            progressBar1.Value = 0;
            //progressBar1.MarqueeAnimationSpeed = 10;

            if (enabled)
                textBoxParam.Focus();

        }

        private void DeleteCancellationToken()
        {
            CancelCancellationToken();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }

        private void CancelCancellationToken()
        {
            _cancellationTokenSource?.Cancel(true);
        }

        #endregion
    }
}
