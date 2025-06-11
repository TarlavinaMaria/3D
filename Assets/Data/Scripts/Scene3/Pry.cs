using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Pry : MonoBehaviour
{
    private Animator _animation;
    private void Awake()
    {
        _animation = GetComponent<Animator>();
    }
    public void Press()
    {
        _animation.SetTrigger("Press");
    }
}
