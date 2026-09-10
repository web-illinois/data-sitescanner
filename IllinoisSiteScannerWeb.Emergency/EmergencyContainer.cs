namespace IllinoisSiteScannerWeb.Emergency {

    public class EmergencyContainer {
        private readonly Func<Alert> _action;
        private Alert _alert;
        private const int NumberOfSecondsInCacheAlert = 5;
        private const int NumberOfSecondsInCacheSafe = 30;
        private bool IsPendingCall = false;

        public EmergencyContainer(Func<Alert> action) {
            _action = action;
            _alert = new();
        }

        public Alert Get() {
            if (!IsPendingCall || DateTime.Now.Subtract(_alert.LastUpdated).TotalSeconds > NumberOfSecondsInCache) {
                IsPendingCall = true;
                _alert = _action();
                IsPendingCall = false;
            }
            return _alert;
        }

        private int NumberOfSecondsInCache => _alert.IsSafe ? NumberOfSecondsInCacheSafe : NumberOfSecondsInCacheAlert;
    }
}