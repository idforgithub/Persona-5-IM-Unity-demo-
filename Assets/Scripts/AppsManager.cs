using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AppsManager : MonoBehaviour
{
    public GameObject canvasListChat;
    public GameObject canvasChat;
    public GameObject panelListChat;
    public Image preventMultiClick;

#region BOX REVERSE ATTRIBUTE
    public GameObject boxImageReverseTransparent;
    public Vector2 sizeDeltaBoxAnim;
    public Vector3 localPositionBoxAnim;
#endregion

    void Update(){
        // Set Activate Panel List Chat when press backbutton in Layout Chat Activate
        if(canvasChat.activeInHierarchy == true){
            if (Input.GetKey(KeyCode.Escape)) {
                PreventMultiClick(false);
                PlayBoxAnimationReverse();
                ActiveCanvas(true, panelListChat, .5f);
                DeactivateCanvasChat(0);
                ActivateCanvasListChat(.5f);
            }
        }
    }

    public void PreventMultiClick(bool set){
        preventMultiClick.raycastTarget = set;
    }

    public void DeactivateCanvasListChat(float time){
        ActiveCanvas(false, canvasListChat, time);
    }

    public void ActivateCanvasListChat(float time){
        ActiveCanvas(true, canvasListChat, time);
    }

    public void DeactivateCanvasChat(float time){
        ActiveCanvas(false, canvasChat, time);
    }

    public void ActivateCanvasChat(float time){
        ActiveCanvas(true, canvasChat, time);
    }

    public void PassingBoxAnimation(GameObject boxAnim){
        RectTransform rect = boxAnim.GetComponent<RectTransform>();
        sizeDeltaBoxAnim = rect.sizeDelta;
        localPositionBoxAnim = rect.localPosition;
    }

    public void DestroyGameObject(GameObject canvas, float time){
        if(time > 0){
            Dictionary<GameObject, float> newPass = new Dictionary<GameObject, float>();
            newPass.Add(canvas, time);
            StartCoroutine("WaitTimeDestroy", newPass);
        }else{
            Destroy(canvas);
        }
    }

    private void PlayBoxAnimationReverse(){
        if(boxImageReverseTransparent.activeInHierarchy){
            ActiveCanvas(false, boxImageReverseTransparent, 1f);
        }else{
            ActiveCanvas(true, boxImageReverseTransparent, 0f);
        }
        boxImageReverseTransparent.GetComponent<RectTransform>().localPosition = localPositionBoxAnim;
        boxImageReverseTransparent.GetComponent<RectTransform>().sizeDelta = sizeDeltaBoxAnim;
        boxImageReverseTransparent.GetComponent<Animation>().Play();
        
    }

    private IEnumerator WaitTimeDestroy(Dictionary<GameObject, float> passValue){
        GameObject passGO = null;
        float passTime = 0;
        foreach (var item in passValue)
        {
            passGO = item.Key;
            passTime = item.Value;
        }
        yield return new WaitForSeconds(passTime);
        Destroy(passGO);
    }

    private void ActiveCanvas(bool setActive, GameObject canvas, float time){
        if(time == 0){
            canvas.SetActive(setActive);
            return;
        }
        Dictionary<string, object> newDict = new Dictionary<string, object>();

        newDict.Add("Canvas", canvas);
        newDict.Add("Time", time);
        newDict.Add("Set", setActive);
        StartCoroutine("WaitTimeCanvas", newDict);
    }

    private IEnumerator WaitTimeCanvas(Dictionary<string, object> passValue){
        GameObject passGO = null;
        float passTime = 0;
        bool passSet = false;
        foreach (var item in passValue)
        {
            passGO = item.Key == "Canvas" ? (GameObject)item.Value : passGO;
            passTime = item.Key == "Time" ? (float)item.Value : passTime;
            passSet = item.Key == "Set"   ? (bool)item.Value : passSet;
        }
        if(passGO == null && passTime == 0){
            throw new System.Exception("Some field for passing not capable");
        }
        yield return new WaitForSeconds(passTime);
        passGO.SetActive(passSet);
    }
}
