using UnityEngine;

public class Quest : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<QuestItem>(out QuestItem questItem))
        {
            questItem.transform.position = transform.position;
            Debug.Log("Quest start");

        }
    }
}
