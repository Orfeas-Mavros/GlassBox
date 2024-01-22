using UnityEngine;
using UnityEngine.SceneManagement;

public class UserInterfaceLogic : MonoBehaviour
{
    public void ToNetworkEditor()
    {
        SceneManager.LoadScene("NetworkEditor");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
