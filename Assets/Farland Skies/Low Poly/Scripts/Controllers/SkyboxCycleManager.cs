using Borodar.FarlandSkies.Core.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Borodar.FarlandSkies.LowPoly
{
    [ExecuteInEditMode]
    [HelpURL("http://www.borodar.com/stuff/farlandskies/lowpoly/docs/QuickStart_v2.5.2.pdf")]
    public class SkyboxCycleManager : Singleton<SkyboxCycleManager>
    {
        [Tooltip("Day-night cycle duration from 0% to 100% (in seconds)")]
        public float CycleDuration = 10f;

        [Tooltip("Current time of day (in percents)")]
        public float CycleProgress;

        public bool Paused;

        private SkyboxDayNightCycle _dayNightCycle;

        //---------------------------------------------------------------------
        // Messages
        //---------------------------------------------------------------------

        protected void Start()
        {
            _dayNightCycle = SkyboxDayNightCycle.Instance;
            UpdateTimeOfDay();

            //disable player controller and make him look at east
        }

        protected void Update()
        {
            float delta_day = CycleProgress;
            if (Application.isPlaying && !Paused)
            {
                CycleProgress += (Time.deltaTime / CycleDuration) * 100f;
                CycleProgress %= 100f;
            }

            UpdateTimeOfDay();

            //Debug.Log("time day");
            //Debug.Log(_dayNightCycle.TimeOfDay);
            //Debug.Log("cycle proges");
            //Debug.Log(CycleProgress);
            //Debug.Log(CycleDuration);
            //Debug.Log("");

            //say: NEW DAY
            //get day and night values from other script
            //if  (delta_day <= 25 && 25 <= CycleProgress)
            if  (delta_day <= _dayNightCycle.GetSunrise() && _dayNightCycle.GetSunrise() <= CycleProgress)
            {
                Debug.Log("new day");

                //enable player controller
            }
            else if ((delta_day <= _dayNightCycle.GetSunset() && _dayNightCycle.GetSunset() <= CycleProgress))
            //else if ((delta_day <= 85 && 85 <= CycleProgress))
            {
                //Debug.Log("new night");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            }

            //get time before and enable player controller shit


            //say: NEW NIGHT
        }

        protected void OnValidate()
        {
            UpdateTimeOfDay();
        }

        //---------------------------------------------------------------------
        // Helpers
        //---------------------------------------------------------------------

        private void UpdateTimeOfDay()
        {
            if (_dayNightCycle != null)
                _dayNightCycle.TimeOfDay = CycleProgress;
        }
    }
}