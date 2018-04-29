using System;
using System.Collections.Generic;
using Starship.Unity.Scheduling;
using UnityEngine;

namespace Starship.Unity.Movement {
    public interface IsMovementController {

        List<Vector3> GetValidMoves();

        Promise Move(Vector3 destination);

        Promise Face(Vector3 direction);

        event Action OnFinishedMoving;
    }
}