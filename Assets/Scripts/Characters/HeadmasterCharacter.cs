using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadmasterCharacter : EnemyCharacter {

    [SerializeField] private List<Transform> CultistSpawns;
    private EnemyCharacter[] Cultists;
    private bool turnEnd = false;

    public override IEnumerator GetTurn () {
        if (turnEnd) {
            for (int i = 0; i < Cultists.Length; i++) {
                if (Cultists[i] != null) {
                    yield return Cultists[i].GetTurn();
                }
            }
        }
        else {
            if (Action != Enums.Action.Stunned) {
                if (deck.CardList.Count == 0) deck.Reshuffle();
                CardToPlay = deck.Draw();
                yield return CombatUIManager.Instance.RevealCard(CardToPlay); //Should extend this time when not testing
                Debug.Log($"{name} playing card {CardToPlay.Name}");
                CombatUIManager.Instance.DisplayMessage($"{name} plays {CardToPlay.Name}");
                yield return CardToPlay.Activate();
            }
        }
        turnEnd = !turnEnd;
    }

    public IEnumerator SpawnStudent () {

        if (Cultists != null)
        {
            for (int i = 0; i < Cultists.Length; i++)
            {
                if (Cultists[i] == null)
                {
                    AfflictedStudentCharacter newStudent = Instantiate(Resources.Load<AfflictedStudentCharacter>("prefabs/Afflicted Student"), CultistSpawns[i].transform);
                    newStudent.transform.localPosition = Vector3.zero;
                    yield return CombatUIManager.Instance.DisplayMessage("An Afflicted Student has come to the Headmaster's aid");
                    Cultists[i] = newStudent;
                    GameManager.manager.foes.Add(newStudent);
                    break;
                }
            }
        }
        else { Debug.Log($"<Color=red>Error:The Headmaster, spawnstudents function contains a null cultists list. </color>"); }
    }

    public IEnumerator SpawnFaculty()
    {

        if (Cultists != null)
        {
            for (int i = 0; i < Cultists.Length; i++)
            {
                if (Cultists[i] == null)
                {
                    ZealousFacultyCharacter newFaculty = Instantiate(Resources.Load<ZealousFacultyCharacter>("prefabs/Zealous Faculty"), CultistSpawns[i].transform);
                    newFaculty.transform.localPosition = Vector3.zero;
                    yield return CombatUIManager.Instance.DisplayMessage("A Zealous Faculty has come to the Headmaster's aid");
                    Cultists[i] = newFaculty;
                    GameManager.manager.foes.Add(newFaculty);
                    break;
                }
            }
        }
        else { Debug.Log($"<Color=red>Error:The Headmaster, spawnfaculty function contains a null cultists list. </color>"); }
    }
}
