using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEventManager : MonoBehaviour
{
    public string nextScene;
    public Animator anim;

    public GameObject settingsMenu;
    private AudioClip buttonClick;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (settingsMenu.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape)) 
        {
            settingsMenu.SetActive(false);
        }
    }

    public void playSound(AudioClip buttonClick) 
    {
        SoundEffects.instance.PlaySFX(buttonClick, 5);
    }

    public void QuitApp() 
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
    public void ChangeSceneWithDelay(string sceneName)
    {
        StartCoroutine(WaitAndLoad(sceneName));
    }

    public IEnumerator WaitAndLoad(string sceneName)
    {
        anim.SetBool("Transition", true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }

}
