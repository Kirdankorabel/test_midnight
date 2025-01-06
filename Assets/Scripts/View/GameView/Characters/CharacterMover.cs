using Controller.Characters;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace View.Game.Characters
{
    public class CharacterMover : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;

        private TimeManager _timeManager;

        public void Construct()
        {
            _timeManager = GameContext.DIContainer.Resolve<TimeManager>();
        }

        public void MoveToTarget(Transform targetTransform, Action action)
        {
            _agent.enabled = true;
            StartCoroutine(MoveToTargetCorutine(targetTransform, action));
        }

        public void MoveToPoint(Vector3 point, Action action)
        {
            _agent.enabled = true;
            _agent.SetDestination(point);
            StartCoroutine(MoveCorutine(point, action));
        }

        public void WaitToTime(float time, Action action)
        {
            _agent.enabled = true;
            StartCoroutine(WaitToTimeCorutine(time, action));
        }
        public void WaitTime(float time, Action action)
        {
            _agent.enabled = true;
            StartCoroutine(WaitTimeCorutine(time, action));
        }

        public void Destroy()
        {
            StopAllCoroutines();
            Destroy(this.gameObject);
        }

        private IEnumerator MoveCorutine(Vector3 point, Action action)
        {
            var position = new Vector3(point.x, transform.position.y, point.z);
            while (Vector3.SqrMagnitude(transform.position - position) > 0.01f)
            {
                _agent.enabled = _timeManager.TimeIsRunning;
                position = new Vector3(point.x, transform.position.y, point.z);
                yield return null;
            }
            action?.Invoke();
        }

        private IEnumerator MoveToTargetCorutine(Transform targetTransform, Action action)
        {
            var position = new Vector3(targetTransform.position.x, transform.position.y, targetTransform.position.z);
            _agent.SetDestination(position);
            while (Vector3.SqrMagnitude(transform.position - position) > 4f)
            {
                _agent.enabled = _timeManager.TimeIsRunning;
                if (Input.GetMouseButtonUp(0) && _timeManager.TimeIsRunning)
                {
                    if (_timeManager.TimeIsRunning)
                    {
                        position = new Vector3(targetTransform.position.x, transform.position.y, targetTransform.position.z);
                        _agent.SetDestination(position);
                    }
                }
                yield return null;
            }
            action?.Invoke();
        }

        private IEnumerator WaitToTimeCorutine(float time, Action action)
        {
            var waitTime = _timeManager.Time + time;
            while (_timeManager.Time < waitTime)
            {
                yield return null;
            }
            action?.Invoke();
        }

        private IEnumerator WaitTimeCorutine(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action?.Invoke();
        }
    }
}