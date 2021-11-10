using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleboxCharacter : EnemyCharacter {

    //===DECK SETUP===//
    //0  - Dark Insight
    //1  - Confound
    //2  - Volatile Ejections
    //3  - Dominating Will
    //4  - Vengeful Retaliation
    //5  - Sear Thoughts
    //6  - Sigil of the Discarded
    //7  - Strangulate
    //8  - Crimson Mark
    //9  - Voracious Hunger
    //10 - Flesh from Bone
    //11 - Coalescing Prism
    //12 - Chromatic Shattering
    //13 - Shifting Variance

    private Enums.PuzzleBoxConfigurations currentConfiguration = Enums.PuzzleBoxConfigurations.Default;
    private bool turnEnd = false;

    private bool AchieverConfig = true;
    private bool ExplorerConfig = true;
    private bool KillerConfig = true;
    private bool SocializerConfig = true;

    private int configCount = 0;
    private bool changingConfig = false;

    public Enums.PuzzleBoxConfigurations Configuration { get { return currentConfiguration; } }

    public override IEnumerator GetTurn () {
        changingConfig = false;
        if (Action != Enums.Action.Stunned) {
            if (deck.CardList.Count == 0) {
                deck.Reshuffle();
            }

            //Check if current configuration is valid
            if ((currentConfiguration == Enums.PuzzleBoxConfigurations.Achiever && !AchieverConfig)
                || (currentConfiguration == Enums.PuzzleBoxConfigurations.Explorer && !ExplorerConfig)
                || (currentConfiguration == Enums.PuzzleBoxConfigurations.Killer && !KillerConfig)
                || (currentConfiguration == Enums.PuzzleBoxConfigurations.Socializer && !SocializerConfig))
                ChangeConfiguration();

            //Determine if this is action 1 or 2
            if (turnEnd == false) {
                int cardChoice;
                //Find configuration to determine cards to play
                switch (currentConfiguration) {
                    //Default Configuration
                    case Enums.PuzzleBoxConfigurations.Default:
                        //In default configuration, play card "Dark Insight"
                        CardToPlay = deck.CardList[0];
                        break;
                    //Achiever Configuration
                    case Enums.PuzzleBoxConfigurations.Achiever:
                        cardChoice = Random.Range(1, 100);
                        break;
                    //Explorer Configuration
                    case Enums.PuzzleBoxConfigurations.Explorer:
                        cardChoice = Random.Range(1, 100);
                        //Randomly play "Searing Thoughts" (25%), "Sigil of the Discarded" (37%) or "" (38%)
                        if (cardChoice <= 25)
                            CardToPlay = deck.CardList[5];
                        else if (cardChoice <= 62)
                            CardToPlay = deck.CardList[6];
                        else
                            CardToPlay = deck.CardList[7];
                        break;
                    //Killer Configuration
                    case Enums.PuzzleBoxConfigurations.Killer:
                        //If first turn as Killer, play "Crimson Mark"
                        if (configCount == 0) {
                            CardToPlay = deck.CardList[8];
                            break;
                        }
                        //Otherwise, play either "Voracious Hunger" or "Flesh from Bone"
                        cardChoice = Random.Range(1, 100);
                        if (cardChoice <= 50)
                            CardToPlay = deck.CardList[9];
                        else
                            CardToPlay = deck.CardList[10];
                        break;
                    //Socializer Configuration
                    case Enums.PuzzleBoxConfigurations.Socializer:
                        //If there are 5 Shards of Eternity, play "Coalescing  Prism"
                        if (GameManager.manager.foes.Count >= 6)
                            CardToPlay = deck.CardList[11];
                        //Otherwise, if first turn as Socializer, play "Chromatic Shatter"
                        else if (configCount == 0)
                            CardToPlay = deck.CardList[12];
                        //Otherwise, play "Shifting Variance"
                        else
                            CardToPlay = deck.CardList[13];
                        break;
                }
            } else {
                //If cube has been in configuration for 2 turns, change configuration.
                //Else, play "Confound"
                if (configCount == 2) {
                    ChangeConfiguration();
                    configCount = 0;
                } else {
                    CardToPlay = deck.CardList[1];
                    configCount++;
                }
            }
            if (!changingConfig) {
                yield return CombatUIManager.Instance.RevealCard(CardToPlay);

                Debug.Log($"{name} playing card {CardToPlay.Name}");
                CombatUIManager.Instance.DisplayMessage($"{name} plays {CardToPlay.Name}");
                yield return CardToPlay.Activate();
            }
            turnEnd = !turnEnd;
        }
    }

    private IEnumerator ChangeConfiguration() {
        bool valid = false;
        int newConfig = 0;
        do {
            newConfig = Random.Range(1, 4);
            if (newConfig == (int)currentConfiguration)
                continue;
            if (newConfig == 1 && !AchieverConfig)
                continue;
            if (newConfig == 2 && !ExplorerConfig)
                continue;
            if (newConfig == 3 && !KillerConfig)
                continue;
            if (newConfig == 4 && !SocializerConfig)
                continue;
            valid = true;
        } while (valid == false);
        currentConfiguration = (Enums.PuzzleBoxConfigurations)newConfig;
        Animator.SetBool("Achiever", false);
        Animator.SetBool("Explorer", false);
        Animator.SetBool("Killer", false);
        Animator.SetBool("Socializer", false);
        yield return new WaitForSeconds(2f);
        switch (currentConfiguration) {
            case Enums.PuzzleBoxConfigurations.Achiever:
                Animator.SetBool("Achiever", true);
                yield return CombatUIManager.Instance.DisplayMessage("The Puzzle Box switches to the Achiever configuration");
                break;
            case Enums.PuzzleBoxConfigurations.Explorer:
                Animator.SetBool("Explorer", true);
                yield return CombatUIManager.Instance.DisplayMessage("The Puzzle Box switches to the Explorer configuration");
                break;
            case Enums.PuzzleBoxConfigurations.Killer:
                Animator.SetBool("Killer", true);
                yield return CombatUIManager.Instance.DisplayMessage("The Puzzle Box switches to the Killer configuration");
                break;
            case Enums.PuzzleBoxConfigurations.Socializer:
                Animator.SetBool("Socializer", true);
                yield return CombatUIManager.Instance.DisplayMessage("The Puzzle Box switches to the Socializer configuration");
                break;
        }
        changingConfig = true;
    }

    public IEnumerator Solve (Enums.PuzzleBoxConfigurations config) {
        switch(config) {
            case Enums.PuzzleBoxConfigurations.Achiever:
                AchieverConfig = false;
                break;
            case Enums.PuzzleBoxConfigurations.Explorer:
                ExplorerConfig = false;
                break;
            case Enums.PuzzleBoxConfigurations.Killer:
                KillerConfig = false;
                break;
            case Enums.PuzzleBoxConfigurations.Socializer:
                SocializerConfig = false;
                break;
        }
        yield return CombatUIManager.Instance.DisplayMessage("The " + config + " configuration has been solved");
        if (!AchieverConfig && !ExplorerConfig && !KillerConfig && !SocializerConfig)
            Health = 0;
    }
}
