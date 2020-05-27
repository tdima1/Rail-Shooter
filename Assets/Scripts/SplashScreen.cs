using UnityEngine;
using UnityEngine.SceneManagement;


public class SplashScreen : MonoBehaviour
{
   private float _rotationAngle = 0;
   
   [SerializeField] public AudioSource _musicPlayer;

   // Start is called before the first frame update
   void Start()
   {
      DontDestroyOnLoad(_musicPlayer);
      Invoke("LoadFirstScene", 3f);
   }

   void Update()
   {
      _rotationAngle += 5f * Time.deltaTime;
      transform.rotation = Quaternion.Euler(_rotationAngle * new Vector3(0, 1, 1));
   }

   private void LoadFirstScene()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }
}
