namespace IllinoisSiteScannerWeb.Emergency {

    public class EmergencyContainer {
        private readonly Func<Alert> _action;
        private Alert _alert;
        private const int NumberOfSecondsInCacheAlert = 5;
        private const int NumberOfSecondsInCacheSafe = 30;
        private const int NumberOfSecondsError = 3600;
        private bool IsPendingCall = false;

        public EmergencyContainer(Func<Alert> action) {
            _action = action;
            _alert = new();
        }

        public Alert Get() {
            try {
                if (!IsPendingCall || DateTime.Now.Subtract(_alert.LastUpdated).TotalSeconds > NumberOfSecondsInCache) {
                    IsPendingCall = true;
                    _alert = _action();
                    IsPendingCall = false;
                }
                // if message is older than 15 minutes, then change the pending call to Safe seconds and blank out the alert
            } catch (Exception e) {
                _alert.LastUpdated = DateTime.Now + TimeSpan.FromSeconds(NumberOfSecondsError);
                _alert.Error = e.ToString();
                throw new InvalidOperationException($"An error occurred while getting the alert -- waiting {NumberOfSecondsError} seconds to retry.", e);
            }
            return _alert;
        }

        private int NumberOfSecondsInCache => _alert.IsSafe ? NumberOfSecondsInCacheSafe : NumberOfSecondsInCacheAlert;
    }
}