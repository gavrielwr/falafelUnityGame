using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level : MonoBehaviour
{
    private const float DIED_ALLOWED = 3f;
    private const float CAMERA_ORTHO_SIZE = 50f;
    private const float INGRADIANTS_MOVE_SPEED = 30f;
    private const float INGRADIANTS_DESTROY_Y_POSITION = -50f;
    private const float INGRADIANTS_SPAWN_Y_POSITION = +50f;
    private const float PITA_POSITION_Y = -40f;
    private const float FALAFEL_MOVE_SPEED = 30f;
    private Dictionary<string, int> ORDERED_INGRADIANTS_AMOUNT = new Dictionary<string, int>()
    {
        {"falafel",6 },
        {"fries",6 },
        {"salad", 3 }
    };

    

    
    private static Level instance;

    public static Level GetInstance()
    {
        return instance;
    }

    private List<Transform> falafelList;
    private List<Transform> chiliList;
    private List<Transform> saladList;
    private List<Transform> friesList;
    private List<Transform> corianderList;
    private List<Transform> eggplantList;
    //private List<Transform> ingradiantsList;
    private float ingradiantsSpawnTimer;
    private float ingradiantsSpawnTimerMax;
    private int ingradiantsSpawned;
    private int ingradiantsPassedCount;
    private int [] ingRequest = { 3, 2, 2 };
    //private Random rndX = new Random();
    private State state;

    public enum State
    {
        WaitingToStart,
        Playing,
        PitaDead
    }

    void Awake(){
        instance = this;
        falafelList = new List<Transform>();
        chiliList = new List<Transform>();
        saladList = new List<Transform>();
        corianderList = new List<Transform>();
        eggplantList = new List<Transform>();
        friesList = new List<Transform>();
        ingradiantsSpawnTimerMax = 2f;
        state = State.WaitingToStart;
    }
    void Start()
    {  
        Pita.GetInstance().OnDied += Pita_OnDied;
        Pita.GetInstance().OnStartedPlaying += Pita_OnStartedPlaying;
        
    }

    private void Pita_OnStartedPlaying(object sender, System.EventArgs e)
    {
        state = State.Playing;
    }
    
    private void Pita_OnDied(object sender, System.EventArgs e)
    {
        state = State.PitaDead;
    }

    void Update()
    {
        if(state == State.Playing)
        {
            Debug.Log("started playing");
             HandleIngrediantsMovment();
             HandleIngrediantsSpawning();
        }
    }

    private void HandleIngrediantsSpawning()
    {
        ingradiantsSpawnTimer -= Time.deltaTime;
        if (ingradiantsSpawnTimer < 0)
        {
            // Time to spawn another Pipe
            ingradiantsSpawnTimer += ingradiantsSpawnTimerMax;
            float xPosition = Random.Range(-100f,100f);
            int ingradiant = Random.Range(0, 6);
            // float heightEdgeLimit = 10f;
            // float minHeight = gapSize * .5f + heightEdgeLimit;
            // float totalHeight = CAMERA_ORTHO_SIZE * 2f;
            // float maxHeight = totalHeight - gapSize * .5f - heightEdgeLimit;

            // float height = Random.Range(minHeight, maxHeight);
            
            //CreateFalafel(xPosition);
            CreateIngradiants(xPosition,ingradiant);
        }
    }

    public void CreateIngradiants(float xPosition,int ingradiant)
    {
        switch(ingradiant)
        {
            case 0: CreateFalafel(xPosition);
                break;
            case 1:
                CreateChili(xPosition);
                break;
            case 2:
                CreateFries(xPosition);
                break;
            case 3:
                CreateEggplant(xPosition);
                break;
            case 4:
                CreateSalad(xPosition);
                break;
            case 5:
                CreateCoriander(xPosition);
                break;
            case 6:
                CreateFalafel(xPosition);
                break;

        }

    }

    public void CreateFalafel(float xPosition)
    {

        Transform falafel = Instantiate(GameAssets.GetInstance().pfFalafel);
        falafel.position = new Vector3(xPosition, INGRADIANTS_SPAWN_Y_POSITION);
        falafel.localScale = new Vector3(5,5,5);
        falafelList.Add(falafel);
        SpriteRenderer falafelSpriteRender = falafel.GetComponent<SpriteRenderer>();
        falafelSpriteRender.size = new Vector2(5, 5);

        BoxCollider2D falafelBoxCollider = falafel.GetComponent<BoxCollider2D>();
        falafelBoxCollider.size = new Vector2(1, 1);
    }

    public void CreateChili(float xPosition)
    {

        Transform chili = Instantiate(GameAssets.GetInstance().pfChili);
        chili.position = new Vector3(xPosition, INGRADIANTS_SPAWN_Y_POSITION);
        chili.localScale = new Vector3(3, 3, 1);
        chiliList.Add(chili);
        SpriteRenderer chiliSpriteRender = chili.GetComponent<SpriteRenderer>();
        chiliSpriteRender.size = new Vector2(5, 5);

        BoxCollider2D chiliBoxCollider = chili.GetComponent<BoxCollider2D>();
        chiliBoxCollider.size = new Vector2(1, 1);
    }

    public void CreateFries(float xPosition)
    {

        Transform fries = Instantiate(GameAssets.GetInstance().pfFries);
        fries.position = new Vector3(xPosition, INGRADIANTS_SPAWN_Y_POSITION);
        fries.localScale = new Vector3(3, 3, 1);
        friesList.Add(fries);
        SpriteRenderer friesSpriteRender = fries.GetComponent<SpriteRenderer>();
        friesSpriteRender.size = new Vector2(5, 5);

        BoxCollider2D friesBoxCollider = fries.GetComponent<BoxCollider2D>();
        friesBoxCollider.size = new Vector2(1, 1);
    }
    public void CreateEggplant(float xPosition)
    {

        Transform eggplant = Instantiate(GameAssets.GetInstance().pfEggplant);
        eggplant.position = new Vector3(xPosition, INGRADIANTS_SPAWN_Y_POSITION);
        eggplant.localScale = new Vector3(3, 3, 1);
        eggplantList.Add(eggplant);
        SpriteRenderer eggplantSpriteRender = eggplant.GetComponent<SpriteRenderer>();
        eggplantSpriteRender.size = new Vector2(5, 5);

        BoxCollider2D eggplantBoxCollider = eggplant.GetComponent<BoxCollider2D>();
        eggplantBoxCollider.size = new Vector2(1, 1);
    }
    public void CreateSalad(float xPosition)
    {

        Transform salad = Instantiate(GameAssets.GetInstance().pfSalad);
        salad.position = new Vector3(xPosition, INGRADIANTS_SPAWN_Y_POSITION);
        salad.localScale = new Vector3(3, 3, 1);
        saladList.Add(salad);
        SpriteRenderer saladSpriteRender = salad.GetComponent<SpriteRenderer>();
        saladSpriteRender.size = new Vector2(5, 5);

        BoxCollider2D saladBoxCollider = salad.GetComponent<BoxCollider2D>();
        saladBoxCollider.size = new Vector2(1, 1);
    }
    public void CreateCoriander(float xPosition)
    {

        Transform coriander = Instantiate(GameAssets.GetInstance().pfCoriander);
        coriander.position = new Vector3(xPosition, INGRADIANTS_SPAWN_Y_POSITION);
        coriander.localScale = new Vector3(1, 1, 1);
        corianderList.Add(coriander);
        SpriteRenderer corianderSpriteRender = coriander.GetComponent<SpriteRenderer>();
        corianderSpriteRender.size = new Vector2(5, 5);

        BoxCollider2D corianderBoxCollider = coriander.GetComponent<BoxCollider2D>();
        corianderBoxCollider.size = new Vector2(1, 1);
    }
    private void HandleIngrediantsMovment()
    {
        for(int i=0; i < falafelList.Count; i++)
        {
            Transform falafel = falafelList[i];
            bool isBelowPita = falafel.position.y < PITA_POSITION_Y;
            falafel.position += new Vector3(0, -1, 0) * FALAFEL_MOVE_SPEED * Time.deltaTime;
           
            if(isBelowPita && falafel.position.y <= PITA_POSITION_Y)
            {
                ingradiantsPassedCount++;
            }
            if (falafel.position.y < INGRADIANTS_DESTROY_Y_POSITION)
            {
                Destroy(falafel.gameObject);
                falafelList.Remove(falafel);
                i--;
            }
        }

        for (int i = 0; i < chiliList.Count; i++)
        {
            Transform chili = chiliList[i];
            bool isBelowPita = chili.position.y < PITA_POSITION_Y;
            chili.position += new Vector3(0, -1, 0) * FALAFEL_MOVE_SPEED * Time.deltaTime;

            if (isBelowPita && chili.position.y <= PITA_POSITION_Y)
            {
                ingradiantsPassedCount++;
            }
            if (chili.position.y < INGRADIANTS_DESTROY_Y_POSITION)
            {
                Destroy(chili.gameObject);
                chiliList.Remove(chili);
                i--;
            }
        }

        for (int i = 0; i < saladList.Count; i++)
        {
            Transform salad = saladList[i];
            bool isBelowPita = salad.position.y < PITA_POSITION_Y;
            salad.position += new Vector3(0, -1, 0) * FALAFEL_MOVE_SPEED * Time.deltaTime;

            if (isBelowPita && salad.position.y <= PITA_POSITION_Y)
            {
                ingradiantsPassedCount++;
            }
            if (salad.position.y < INGRADIANTS_DESTROY_Y_POSITION)
            {
                Destroy(salad.gameObject);
                saladList.Remove(salad);
                i--;
            }
        }

        for (int i = 0; i < friesList.Count; i++)
        {
            Transform fries = friesList[i];
            bool isBelowPita = fries.position.y < PITA_POSITION_Y;
            fries.position += new Vector3(0, -1, 0) * FALAFEL_MOVE_SPEED * Time.deltaTime;

            if (isBelowPita && fries.position.y <= PITA_POSITION_Y)
            {
                ingradiantsPassedCount++;
            }
            if (fries.position.y < INGRADIANTS_DESTROY_Y_POSITION)
            {
                Destroy(fries.gameObject);
                friesList.Remove(fries);
                i--;
            }
        }

        for (int i = 0; i < corianderList.Count; i++)
        {
            Transform coriander = corianderList[i];
            bool isBelowPita = coriander.position.y < PITA_POSITION_Y;
            coriander.position += new Vector3(0, -1, 0) * FALAFEL_MOVE_SPEED * Time.deltaTime;

            if (isBelowPita && coriander.position.y <= PITA_POSITION_Y)
            {
                ingradiantsPassedCount++;
            }
            if (coriander.position.y < INGRADIANTS_DESTROY_Y_POSITION)
            {
                Destroy(coriander.gameObject);
                corianderList.Remove(coriander);
                i--;
            }
        }

        for (int i = 0; i < eggplantList.Count; i++)
        {
            Transform eggplant = eggplantList[i];
            bool isBelowPita = eggplant.position.y < PITA_POSITION_Y;
            eggplant.position += new Vector3(0, -1, 0) * FALAFEL_MOVE_SPEED * Time.deltaTime;

            if (isBelowPita && eggplant.position.y <= PITA_POSITION_Y)
            {
                ingradiantsPassedCount++;
            }
            if (eggplant.position.y < INGRADIANTS_DESTROY_Y_POSITION)
            {
                Destroy(eggplant.gameObject);
                eggplantList.Remove(eggplant);
                i--;
            }
        }

    }


}


