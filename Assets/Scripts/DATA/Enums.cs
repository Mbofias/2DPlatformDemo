using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPlayerType
{
    ZERO, X
}

public enum ECollectableType
{
    BOMB, AERIAL, OVERLOAD
}

public enum GamePageState
{
    NONE,
    TUTORIAL,
    GAME_OVER,
    PAUSE
}

public enum LadderPos
{
    TOP, BOTTOM, MIDDLE, NONE
}