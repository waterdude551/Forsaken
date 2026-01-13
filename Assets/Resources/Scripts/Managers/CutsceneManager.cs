using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameManager manager;

    [Header("Cutscene References")]
    [SerializeField] private GameObject[] cutscenes;
    private BossStateMachine bossStateMachine;
    private PlayerStateMachine playerStateMachine;
    private int currentCutscene;

    void Awake()
    {
        bossStateMachine = boss.GetComponent<BossStateMachine>();
        playerStateMachine = player.GetComponent<PlayerStateMachine>();
    }

    public void PlayCutScene(int index)
    {
        Debug.Log(cutscenes[index].name);
        cutscenes[index].GetComponent<PlayableDirector>().Play();
        currentCutscene = index;
    }

    public void OnCutsceneStart()
    {
        playerStateMachine.OnDisable();
        manager.FightStarted = false;
        bossStateMachine.JumpToState(new BossStartState(bossStateMachine));
    }

    public void OnCutsceneEnd()
    {
        if (!manager.GameOver)
        {
            playerStateMachine.OnEnable();
            manager.FightStarted = true;
        }
        cutscenes[currentCutscene].GetComponent<PlayableDirector>().Stop();
    }
}
