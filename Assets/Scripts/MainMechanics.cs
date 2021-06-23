
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMechanics : MonoBehaviour
{

    public GameObject Character;
    public float thrust;
    public Transform prefab;
    public TimeManager timemanger;

    private Rigidbody2D CharacterBody;
    private bool cantouch = true;
    Vector3 Nbegan = new Vector2(0, 0);
    void Start()
    {
        prefab.gameObject.SetActive(false);
        CharacterBody = Character.GetComponent<Rigidbody2D>();

    }

    void Update()
    {

        int nbTouches = Input.touchCount;

        if (nbTouches > 0)
        {

            for (int i = 0; i < nbTouches; i++)
            {
                Touch touch = Input.GetTouch(0);

                TouchPhase phase = touch.phase;

                switch (phase)
                {
                    case TouchPhase.Began:
                        if (cantouch)
                        {
                            timemanger.DoslowMotion();
                            Nbegan = Camera.main.ScreenToWorldPoint(touch.position);
                        }
                        print("Position BEGAN at : " + Nbegan);
                        break;
                    case TouchPhase.Moved:
                        Vector3 Change = Nbegan - Camera.main.ScreenToWorldPoint(touch.position);
                        if (cantouch)
                        {
                            Drawino(Change);
                            prefab.position = new Vector3(CharacterBody.position.x,CharacterBody.position.y,15);
                        }
                        break;
                    case TouchPhase.Stationary:
                        Change = Nbegan - Camera.main.ScreenToWorldPoint(touch.position);
                        if (cantouch)
                        {
                            prefab.position = new Vector3(CharacterBody.position.x, CharacterBody.position.y, 15);
                            Drawino(Change);
                        }
                        break;
                    case TouchPhase.Ended:
                        Change = Nbegan - Camera.main.ScreenToWorldPoint(touch.position);
                        prefab.gameObject.SetActive(false);
                        timemanger.ReturnNormal();
                        Lunch(Change);
                        break;
                    case TouchPhase.Canceled:
                        prefab.gameObject.SetActive(false);
                        timemanger.ReturnNormal();
                        print("Touch index " + touch.fingerId + " cancelled");
                        break;
                }
            }
        }
    }
    void Drawino(Vector2 ii)
    {

        float aa1 = AngleInDeg(CharacterBody.position, CharacterBody.position - ii);
        prefab.rotation = Quaternion.AngleAxis(aa1 + 90, Vector3.forward);
        float clamped = Mathf.Clamp(ii.magnitude*0.03f,0f,0.12f);
        print("The Clamped Value is :" + clamped);
        prefab.localScale = new Vector3(0.1f,clamped,0);
        prefab.gameObject.SetActive(true);
    }
    void Lunch(Vector2 ii)
    {
        if (cantouch)
        {
            CharacterBody.velocity = Vector3.zero;
            CharacterBody.angularVelocity = 0f;
    
            Vector2 Clamped = Vector2.ClampMagnitude(ii, 4);
            print("The Force of the shit is :" + Clamped);

            CharacterBody.AddForce(Clamped * thrust, ForceMode2D.Impulse);
            cantouch = false;
        }
    }
    public static float AngleInDeg(Vector3 vec1, Vector3 vec2)
    {
        return AngleInRad(vec1, vec2) * 180 / Mathf.PI;
    }
    public static float AngleInRad(Vector3 vec1, Vector3 vec2)
    {
        return Mathf.Atan2(vec2.y - vec1.y, vec2.x - vec1.x);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Tile")
        {
            cantouch = true;
        }

        if (collision.collider.tag == "BSides")
        {
            SceneManager.LoadScene("dunno");
        }
    }
}
