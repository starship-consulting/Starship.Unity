using System;
using System.Collections.Generic;
using Assets.Scripts.Scheduling;
using UnityEngine;

namespace Assets.Scripts.Movement {
    public interface IsMovementController {

        List<Vector3> GetValidMoves();

        Promise Move(Vector3 destination);

        Promise Face(Vector3 direction);

        event Action OnFinishedMoving;
    }
}