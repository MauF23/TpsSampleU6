using System.Collections.Generic; //namespace para usar Listas. 
using UnityEngine;
using System.Linq; //Filtros de búsqueda para listas


public class GameObjectPool : MonoBehaviour
{
    //prefab
    public GameObject gameObjectToPool;

    [SerializeField]
    private List<GameObject> pool = new List<GameObject>(); //Lista de objetos a reutilizar

    [SerializeField]
    private int poolDefaultSize = 50; //tamaño por default del pool

    private void Start()
    {
        //Inicializar pool
        for(int i = 0; i < poolDefaultSize; i++)
        {
            InstancePoolObject();
        }
    }

    /// <summary>
    /// Función para crear nuevos objetos y añadirlos al pool
    /// </summary>
    GameObject InstancePoolObject()
    {
        GameObject newGameObject = Instantiate(gameObjectToPool);
        pool.Add(newGameObject); //añadir al pool
        newGameObject.SetActive(false); //apagarlo
        return newGameObject;
    }

    /// <summary>
    /// Función para obtener un objeto del pool
    /// </summary>
    public GameObject GetGameObjectFromPool()
    {
        //Busca el primer objeto inactivo de la lista
        GameObject target = pool.FirstOrDefault(gameObject => !gameObject.activeSelf);
        
        //Si no encuentra un objeto inactivo, crear uno nuevo
        if(target == null)
        {
            target = InstancePoolObject();
        }

        //Prender el objeto para que realice su comportamiento.
        target.SetActive(true);
        return target;
    }

    /// <summary>
    /// Función sobrecargada de GetGameObjectFromPool, setea la posición del objeto encontrado
    /// </summary>
    public GameObject GetGameObjectFromPool(Vector3 position)
    {
        GameObject target = GetGameObjectFromPool();
        target.transform.position = position;   
        return target;
    }


}
