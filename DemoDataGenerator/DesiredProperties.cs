namespace DemoDataGenerator
{
    public class DesiredProperties
    {
        private bool _sendData = true;
        // in milliseconds
        private int _sendInterval = 5000;
        private int _instanceCount = 2;

        public bool SendData
        {
            get { return _sendData; }
        }
        /// <summary>
        /// in milliseconds
        /// </summary>
        /// <value></value>
        public int SendInterval
        {
            get { return _sendInterval; }
        }
        public int InstanceCount
        {
            get { return _instanceCount; }
        }
    }
}
