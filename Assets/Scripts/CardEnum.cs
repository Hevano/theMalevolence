using System.Collections;
using System.Collections.Generic;
public enum CardEnum {
    MasterPlan,
    NothingButEdge
}
public abstract partial class CardEffect : ICard{
    //Used to map card enum to the children of CardEffect
    public static Dictionary<CardEnum, CardEffect> cardMap;
}
