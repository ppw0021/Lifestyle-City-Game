using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base
    {
        public Base(int user_id)
        {
            user_id_element = user_id;
        }
        public int user_id_element = -1;
        public List<BuildingInstance> buildingList = new List<BuildingInstance>();
    }
