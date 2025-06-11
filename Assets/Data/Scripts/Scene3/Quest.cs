using UnityEngine;

public class Quest : MonoBehaviour
{
    private CubeScene3 _tempCube;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<CubeScene3>(out CubeScene3 item))
        {
            if (item.IsActivated)
            {
                item.transform.position = transform.position;
                item.transform.rotation = Quaternion.identity;
                Destroy(item);
                Debug.Log("Quest start");

            }
        }
    }
}
