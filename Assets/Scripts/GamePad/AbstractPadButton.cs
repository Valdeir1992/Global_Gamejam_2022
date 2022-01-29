using UnityEngine;
using UnityEngine.EventSystems;


public abstract class AbstractPadButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private bool _press;

    private void Start()
    {
        InvokeRepeating("ListenInputs", 0, 0.01f);
    }

    private void ListenInputs()
    {
        if (_press)
        {
            BtnAction();
        }
    }

    protected abstract void BtnAction();

    public void OnPointerEnter(PointerEventData eventData)
    {
        _press = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _press = false;
    }
}
