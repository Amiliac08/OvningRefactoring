using System;
using System.Collections.Generic;
namespace OBP200_PetshopExercise;

public class Animals
{
    public static List<Animals> animals = new List<Animals>();
    public string species { get; private set; }
    public string type { get; private set; }
    public int costs { get; private set; }
    public string sound { get; private set; }

    public Animals(string species, string type, int costs, string sound)
    {
        this.species = species;
        this.type = type;
        this.costs = costs;
        this.sound = sound;
    }

    public static int Count
    {
        get { return animals.Count; }
    }

}

