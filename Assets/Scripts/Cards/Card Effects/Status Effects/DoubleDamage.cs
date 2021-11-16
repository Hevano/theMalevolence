public class DoubleDamage : StatusEffect {
    private Character watchedCharacter;

    public DoubleDamage(Character target){
        watchedCharacter = target;
        watchedCharacter.onAttack += BoostDamage;
        GameManager.manager.onPhaseChange += EndEffect;
    }

    public void BoostDamage(Character target, ref Damage damage){
        var newDamage = new Damage(
            watchedCharacter.data.basicAttack.DieNumber * 2,
            watchedCharacter.data.basicAttack.DieSize,
            watchedCharacter.data.basicAttack.DieBonus * 2
        );
        damage = newDamage;
    }

    public void EndEffect(Enums.GameplayPhase phase){
        if(phase == Enums.GameplayPhase.Draw){
            watchedCharacter.onAttack -= BoostDamage;
            GameManager.manager.onPhaseChange -= EndEffect;
        }
    }

}