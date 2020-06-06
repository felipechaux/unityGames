using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//controlador de niveles procedurales
public class LevelManager : MonoBehaviour {

    //singleton, solo debe haber un level manager 
    public static LevelManager shareInstance;

    //todos los nivles
    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();

    //niveles actualmente en la escena
    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();

    //creacion de primer bloque
    public Transform levelStartPosition; 


    public void Awake()
    {
        if (shareInstance==null)
        {
            shareInstance = this;
        }

    }

    void Start () {
        GenerateInitialBlocks();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //añadir bloques
    public void AddLevelBlock()
    {
        int randomIdx = Random.Range(0,allTheLevelBlocks.Count);

        LevelBlock block;

        Vector3 spawnPosition = Vector3.zero;

        //=0 no se ha añadido ningun bloque
        if (currentLevelBlocks.Count==0)
        {   
            //tomar uno de esos bloques de los disponibles - (se toma primer bloque)
            block = Instantiate(allTheLevelBlocks[0]);
            //calcular posicion de primer bloque
            spawnPosition = levelStartPosition.position;
        }
        else{
            //instanciar bloque que se encuentra en posicion random 
            block = Instantiate(allTheLevelBlocks[randomIdx]);
            //final del bloque anteior - efecto de cadenas - enganchado
            spawnPosition = currentLevelBlocks[currentLevelBlocks.Count - 1].exitPoint.position;
        }


        //bloque actual debe tener como padre el level manager --- false(para que no se mantengan las transformaciones del padre )
        block.transform.SetParent(this.transform,false);

        //el punto de salida de uno concida con el punto de entrada otro bloque
        // transformacion de vectores al restar coordenadas

        Vector3 correction = new Vector3(
            spawnPosition.x-block.startPoint.position.x,
            spawnPosition.y-block.startPoint.position.y,0
            );

        //mover el bloque actual con la correccion
        block.transform.position = correction;
        //añadir bloque a coleccion actual
        currentLevelBlocks.Add(block);

    }

    //destruye el primer bloque incializado dentro de la coleccion
    public void RemoveLevelBlock()
    {
        LevelBlock oldBLock = currentLevelBlocks[0];
        //elimina antiguo
        currentLevelBlocks.Remove(oldBLock);
        //se destruye para que desaparezca de la pantalla
        Destroy(oldBLock.gameObject);


    }
    public void RemoveAllLevelBlocks()
    {
        while (currentLevelBlocks.Count>0)
        {
            RemoveLevelBlock();
        }

    }
    
    public void GenerateInitialBlocks()
    {
        for(int i=0; i < 3; i++)
        {
            AddLevelBlock();
        }
    }
}
