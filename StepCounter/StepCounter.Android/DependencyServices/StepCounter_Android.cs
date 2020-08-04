
using Android.App;
using Android.Content;
using Android.Hardware;
using Android.Runtime;
using StepCounter.DependencyServices;
using StepCounter.Droid.DependencyServices;

[assembly: Xamarin.Forms.Dependency(typeof(StepCounter_Android))]
namespace StepCounter.Droid.DependencyServices
{
    public class StepCounter_Android : Java.Lang.Object, ISensorEventListener, IStepCounter
    {
        readonly SensorManager sensorManager;
        Sensor stepCounter;

        private int steps;
        public int Steps
        {
            get { return steps; }
            set { Steps = value; }
        }

        public StepCounter_Android() : base()
        {
            sensorManager = (SensorManager)Application.Context.GetSystemService(Context.SensorService);
            stepCounter = sensorManager.GetDefaultSensor(SensorType.StepCounter);
        }

        public event StepCountChangedEventHandler StepCountChanged;

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
        }

        public void OnSensorChanged(SensorEvent e)
        {
            steps++;
            StepCountChanged(this, new StepCountChangedEventArgs {
                Value = Steps
            });
        }

        public void Start()
        {
            if (stepCounter != null)
            {
                steps = 0;

                sensorManager.RegisterListener(this, stepCounter, SensorDelay.Normal);
            }
        }

        public void Stop()
        {
            if (stepCounter != null)
            {
                sensorManager.UnregisterListener(this, stepCounter);
            }
        }

        public bool IsAvailable() => stepCounter != null;
    }
}