using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelManagerInitable {

    void LevelLoaded(int level);

    void LevelUnlaoded();
}
