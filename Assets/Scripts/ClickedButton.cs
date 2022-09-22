using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickedButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _default,_pressed;
    [SerializeField] private AudioClip _compressedClip, _uncompressedClip;
    [SerializeField] private AudioSource _source;


   [SerializeField] public Animator tanim;
   [SerializeField] string sceneName;

    // these are effectively the same thing as onMouseDown and onMouseUp
    public void OnPointerDown(PointerEventData eventData)
    {
        // When user taps the button it changes its state to pressed
        _img.sprite = _pressed;
        _source.PlayOneShot(_compressedClip);
      
       StartCoroutine(LoadScene());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // When user taps the button it changes its state to unpressed
        _img.sprite = _default;
        _source.PlayOneShot(_uncompressedClip);
    }


    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1.5f);
        tanim.SetTrigger("end");
        yield return new WaitForSeconds(1.7f);
         SceneManager.LoadScene(sceneName);
    }


}
