using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTS.PlayScene
{
    public enum FlyingDirection { Left, Up };

    public class FlyingBlock : Block
    {
        public FlyingDirection direction;

        public int WarningIndex { get; set; }
        public float WarningTime { get; set; }

        private void Start()
        {
            StartCoroutine(MoveCoroutine());
        }

        private IEnumerator MoveCoroutine()
        {
            GetWarningObject().SetActive(true);
            yield return new WaitForSeconds(WarningTime);
            GetWarningObject().SetActive(false);

            Vector3 directionValue = direction == FlyingDirection.Left ? Vector3.left : Vector3.up;

            while (!ShouldDestroy())
            {
                transform.Translate(Player.Instance.MoveSpeed * Time.smoothDeltaTime * directionValue);
                yield return null;
            }

            Destroy(transform.gameObject);
        }

        private GameObject GetWarningObject()
        {
            GameObject target;

            if (direction == FlyingDirection.Left)
                target = UIController.Instance.GetRightWarning(WarningIndex);
            else
                target = UIController.Instance.GetDownWarning(WarningIndex);

            return target;
        }

        private bool ShouldDestroy()
        {
            if (direction == FlyingDirection.Left && transform.position.x < -64)
                return true;

            if (direction == FlyingDirection.Up && transform.position.y > 32)
                return true;

            return false;
        }

        public void Destroy()
        {
            UIController.Instance.GetRightWarning(WarningIndex).SetActive(false);
            Destroy(transform.gameObject);
        }
    }
}