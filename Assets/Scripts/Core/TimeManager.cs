using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using View.UI;

namespace View
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private TimePanel _timePanel;
        [SerializeField] private Button _waitButton;

        [Header("length of day in min")]
        [SerializeField] private float _dayLeght = 12f;

        private int _hour = 1;
        private int _day;
        private float _time;
        private float _lastTime = -1;
        private float _nextHourTime;
        private float _nextDayTime;

        public bool TimeIsRunning { get; private set; } = true;

        public float Time => _time;
        public int Day => _day;
        public int Hour => _hour;
        public float TimeSpeed => 1f;

        public event System.Action<int> OnHourChanded;
        public event System.Action<int> OnDayChanded;

        public void InitializeNew()
        {
            SetData(0);
        }

        public void AddTime(float time)
        {
            _time += time;
            if (_time > _nextDayTime)
            {
                _day = (int)_time + 1;
                _nextDayTime = _time + 1f;
                OnDayChanded?.Invoke(_day);
                OnHourChanded?.Invoke(_hour);
            }
            if (_time > _nextHourTime)
            {
                _hour = (int)((_time - (int)_time) * 24);
                _nextHourTime = _time + 1f / 24f;
                OnHourChanded?.Invoke(_hour);
            }
            _timePanel.SetTime(_time);
            _lastTime = _time;
        }

        public void SetData(float time)
        {
            StopAllCoroutines();
            _time = time;
            _timePanel.SetTime(_time);
            StartCoroutine(TimeCorutine());
        }

        private IEnumerator TimeCorutine()
        {
            var koeficient = 1f / (_dayLeght * 60f);
            while (true)
            {
                if (TimeIsRunning)
                {
                    AddTime(UnityEngine.Time.deltaTime * koeficient * UnityEngine.Time.timeScale);
                }
                yield return null;
            }
        }
    }
}