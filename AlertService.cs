
using Microsoft.JSInterop;

namespace LabScript
{
    public class AlertService : IAsyncDisposable, IAlertService
    {

        readonly Lazy<Task<IJSObjectReference>> ijsObjectReference;

        public AlertService(IJSRuntime ijsruntime)
        {
            this.ijsObjectReference = new Lazy<Task<IJSObjectReference>>(() =>
                ijsruntime.InvokeAsync<IJSObjectReference>("import", "./Home.razor.js").AsTask()
            );
        }

        public async ValueTask DisposeAsync()
        {
            if (ijsObjectReference.IsValueCreated)
            {
                IJSObjectReference moduleJs = await ijsObjectReference.Value;
                await moduleJs.DisposeAsync();
            }
        }

        public async Task CallJsAlertFunction()
        {
            var jsModule = await ijsObjectReference.Value;
            await jsModule.InvokeVoidAsync("jsFuncion");
        }
    }
}
