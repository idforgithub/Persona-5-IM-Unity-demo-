using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AppsManager : MonoBehaviour
{
    public GameObject canvasListChat;
    public GameObject canvasChat;
    public GameObject panelListUser;
    public Image preventMultiClick;

#region ANIMATION --- BOX REVERSE ATTRIBUTE
    public GameObject boxImageReverseTransparent;
    private Vector3 localPositionBoxAnim;
#endregion

    private User userPasser;

    /*
    bool palindrome(string value){
        for(int i = 0; i < value.Length / 2; i++){
            if(value[i] == value[(value.Length-1) - i]){
               continue; 
            }else{
                return false;
            }
        }
        return true;
    }

    bool palindromeRecursive(string value){
        return palindromeRecursive(value, 0);
    }

    bool palindromeRecursive(string value, int i){
        if(i < value.Length / 2){
            if(value[i] == value[(value.Length-1) - i]){
               return palindromeRecursive(value, i + 1); 
            }else{
                return false;
            }
        }
        return true;
    }
    */

    void Awake(){
        ToUser();
    }

    void FixedUpdate(){
        // Set Activate Panel List Chat when press backbutton in chat layout
        if(canvasChat.activeInHierarchy == true){
            if (Input.GetKey(KeyCode.Escape)) {
                ToUser(false);
            }
        }
    }

    private void ToUser(){
        ToUser(true);
    }

    private void ToUser(bool firstRun){
        if(firstRun){
            GetComponent<Spawnner>().LoadUser();
        } else {
            PlayBoxAnimationReverse();
        }

        // Load Animation Layout
        PreventMultiClick(false);
        
        ActivatedGameObject(true, panelListUser, .5f);
        DeactivateCanvasChat(0);
        ActivateCanvasListChat(.1f);
    }

    public void ToChat(User toUserChat, GameObject boxAnim){
        GetComponent<SpawnerChat>().LoadChat(toUserChat);

        // Load Animation Layout
        PreventMultiClick(true);
        DestroyGameObject(boxAnim, 1f);
        DeactivateCanvasListChat(.35f);
        ActivateCanvasChat(0.9f);
        PassingBoxAnimation(boxAnim);
    }

    private bool PreventMultiClick(bool set){
        return preventMultiClick.raycastTarget = set;
    }

    private void PassingBoxAnimation(GameObject boxAnim){
        localPositionBoxAnim = boxAnim.GetComponent<RectTransform>().localPosition;
    }

    private void DeactivateCanvasListChat(float time){
        ActivatedGameObject(false, canvasListChat, time);
    }

    private void ActivateCanvasListChat(float time){
        ActivatedGameObject(true, canvasListChat, time);
    }

    private void DeactivateCanvasChat(float time){
        ActivatedGameObject(false, canvasChat, time);
    }

    private void ActivateCanvasChat(float time){
        ActivatedGameObject(true, canvasChat, time);
    }

    private void PlayBoxAnimationReverse(){
        if(boxImageReverseTransparent.activeInHierarchy){
            ActivatedGameObject(false, boxImageReverseTransparent, 1f);
        }else{
            ActivatedGameObject(true, boxImageReverseTransparent, 0f);
        }
        boxImageReverseTransparent.GetComponent<RectTransform>().localPosition = localPositionBoxAnim;
        boxImageReverseTransparent.GetComponent<Animation>().Play();
    }

#region TIMER BOOM
    public void DestroyGameObject(GameObject canvas, float time){
        if(time == 0){
            Destroy(canvas);
            return;
        }

        StartCoroutine("WaitTimeDestroy", new object[]{
            canvas,
            time
        });
    }

    private IEnumerator WaitTimeDestroy(object[] passValue){
        GameObject passGO = (GameObject)passValue[0];
        float passTime = (float)passValue[1];

        yield return new WaitForSeconds(passTime);
        Destroy(passGO);
    }

    private void ActivatedGameObject(bool setActive, GameObject canvas, float time){
        if(time == 0){
            canvas.SetActive(setActive);
            return;
        }

        StartCoroutine("WaitTimeCanvas", new object[]{
            canvas,
            time,
            setActive
        });
    }

    private IEnumerator WaitTimeCanvas(object[] passValue){
        GameObject passGO = (GameObject)passValue[0];
        float passTime = (float)passValue[1];
        bool passSet = (bool)passValue[2];
        
        yield return new WaitForSeconds(passTime);
        passGO.SetActive(passSet);
    }
#endregion
}

