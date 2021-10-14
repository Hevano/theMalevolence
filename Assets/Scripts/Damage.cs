using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ValueRange {
    protected int dieNum;
    protected int dieSize;
    protected int bonus;
    public int Max {
        get {
            return dieNum * dieSize + bonus;
        }
    }

    public int Min {
        get {
            return dieNum + bonus;
        }
    }

    public int Value {
        get {
            int sum = 0;
            for(int i = 0; i < dieNum; i++){
                sum += Random.Range(1, dieSize+1);
            }
            return sum + bonus;
        }
    }

    public ValueRange(int dieNum, int dieSize, int bonus = 0){
        this.dieNum = dieNum;
        this.dieSize = dieSize;
        this.bonus = bonus;
    }
}
[System.Serializable]
public class Damage : ValueRange
{
    public Damage(int dieNum, int dieSize, int bonus = 0) : base(dieNum, dieSize, bonus){
        
    }
}
