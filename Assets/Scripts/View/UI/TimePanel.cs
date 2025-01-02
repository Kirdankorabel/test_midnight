using TMPro;
using UnityEngine;

namespace View.UI
{
    public class TimePanel : ConstructableMonoBehaviour
    {
        [SerializeField] private GameObject _timeView;
        [SerializeField] private TMP_Text _dayText;

        private TimeManager _timeManager;

        public override void Construct()
        {
            _timeManager = GameContext.DIContainer.Resolve<TimeManager>();
        }

        public void SetTime(float time)
        {
            _timeView.transform.eulerAngles = new Vector3(0, 0, 360f * time);
            _dayText.text = _timeManager.Day.ToString();
        }
    }
}