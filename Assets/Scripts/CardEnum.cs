using System.Collections;
using System.Collections.Generic;
public enum CardEnum {
    MasterPlan,
    NothingButEdge,
    Foresight
}
public abstract partial class CardEffect : ICard{
    //Used to map card enum to the children of CardEffect
    public static Dictionary<CardEnum, CardEffect> cardMap = new Dictionary<CardEnum, CardEffect>() {
        {CardEnum.Foresight, new CardEffects.Foresight()}
    };
}
