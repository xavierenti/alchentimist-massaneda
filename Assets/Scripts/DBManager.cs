using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class DBManager : MonoBehaviour
{
    IDbConnection dbConnection;


    private int cantidad = 0;
    private int cantiduby = 0;


    public Transform IngredientGrid;
    public Transform IngredientGridGrimory;
    public GameObject IngredientButton;
    public GameObject IngredientButtonGrimory;

    private bool grim = false;

    

    public Button Grymory;

    private void OpenDatabase()
    {
        string dbUri = "URI=file:alchENTImist.db";        
        dbConnection = new SqliteConnection(dbUri);
        dbConnection.Open();
    }

    public void Ingredientes()
    {
        cantidad = 0;

        string query = "SELECT * FROM ingredients";

        IDbCommand cmd = dbConnection.CreateCommand();
        cmd.CommandText = query;

        IDataReader dataReader = cmd.ExecuteReader();

        while (dataReader.Read())
        {

            List<string> list = new List<string>();

            cantidad = cantidad + 1;
            string Ingredients_name = dataReader.GetString(1);
            Debug.Log(Ingredients_name);
            string Ingredients_cost = dataReader.GetString(2);
            Debug.Log(Ingredients_cost);
            string Ingredients_icon = dataReader.GetString(3);
            Debug.Log(Ingredients_icon);
            string Ingredients_description = dataReader.GetString(4);
            Debug.Log(Ingredients_description);

            GameObject Ingrediente = Instantiate(IngredientButton, IngredientGrid);
            Ingrediente.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = Ingredients_name + Ingredients_cost;
            Ingrediente.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = Ingredients_icon;
            Ingrediente.transform.GetChild(2).transform.GetChild(2).GetComponent<Text>().text = Ingredients_cost;
            Ingrediente.transform.GetChild(3).transform.GetChild(3).GetComponent<Text>().text = Ingredients_description;

        }

    }



    // Start is called before the first frame update
    void Start()
    {



        Debug.Log("AbrirDB");
        OpenDatabase();

        string query = "SELECT * FROM potion_types"; 

        IDbCommand cmd = dbConnection.CreateCommand();
        cmd.CommandText = query;

        IDataReader dataReader = cmd.ExecuteReader();

        while (dataReader.Read())
        {
            string potion_type = dataReader.GetString(1);
            Debug.Log(potion_type);
        }

        Ingredientes();

        Grymory.interactable = true;

        if (grim == false && Grymory.interactable)
        {
            OpenGrymory();
            grim = true;
        }
        else
        {
            Instantiate(IngredientGridGrimory);
            Destroy(IngredientGridGrimory);
            grim = false;
        }
               

    }




    // Update is called once per frame
    void Update()
    {
       
     
    }




    public void OpenGrymory()
    {
        cantidad = 0;
        
        string query = "SELECT * FROM Potions";

        IDbCommand cmd = dbConnection.CreateCommand();
        cmd.CommandText = query;

        IDataReader dataReader = cmd.ExecuteReader();

        while (dataReader.Read())
        {
            cantidad = cantidad + 1;
            string query1 = "SELECT * FROM potions_ingredients WHERE id_potion = " + cantidad;

            IDbCommand cmd1 = dbConnection.CreateCommand();
            cmd.CommandText = query1;

            IDataReader dataReader1 = cmd1.ExecuteReader();

            string quantity = dataReader1.GetString(1);
            Debug.Log(quantity);
            string id_potion = dataReader1.GetString(2);
            Debug.Log(id_potion);
            string id_ingrediente = dataReader.GetString(3);
            Debug.Log(id_ingrediente);

            

            while (dataReader1.Read())
            {
                
                string query2 = "SELECT * FROM potions WHERE id_potion = " + id_potion;

                IDbCommand cmd2 = dbConnection.CreateCommand();
                cmd.CommandText = query2;

                IDataReader dataReader2 = cmd2.ExecuteReader();
                while (dataReader2.Read())
                {
                    string potion_name = dataReader2.GetString(1);
                    string potion_cost = dataReader2.GetString(2);
                    string potion_icon = dataReader2.GetString(3);
                    string potion_description = dataReader2.GetString(4);

                    GameObject Ingrediente = Instantiate(IngredientButton, IngredientGridGrimory);
                    Ingrediente.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "Pocion " + potion_name + potion_cost;
                    Ingrediente.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = potion_icon;
                    Ingrediente.transform.GetChild(2).transform.GetChild(2).GetComponent<Text>().text = potion_cost;
                    Ingrediente.transform.GetChild(3).transform.GetChild(3).GetComponent<Text>().text = potion_description;
                }
                
                string query3 = "SELECT * FROM ingredients WHERE id_ingredient = " + id_ingrediente;

                IDbCommand cmd3 = dbConnection.CreateCommand();
                cmd.CommandText = query3;

                IDataReader dataReader3 = cmd3.ExecuteReader();
                
                while (dataReader3.Read())
                {
                    string Ingredients_name = dataReader3.GetString(1);
                    Debug.Log(Ingredients_name);
                    string Ingredients_cost = dataReader3.GetString(2);
                    Debug.Log(Ingredients_cost);
                    string Ingredients_icon = dataReader3.GetString(3);
                    Debug.Log(Ingredients_icon);
                    string Ingredients_description = dataReader3.GetString(4);
                    Debug.Log(Ingredients_description);


                    GameObject Ingrediente = Instantiate(IngredientButton);
                    Ingrediente.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = quantity +" Ingrediente "+Ingredients_name + Ingredients_cost;
                    Ingrediente.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = Ingredients_icon;
                    Ingrediente.transform.GetChild(2).transform.GetChild(2).GetComponent<Text>().text = Ingredients_cost;
                    Ingrediente.transform.GetChild(3).transform.GetChild(3).GetComponent<Text>().text = Ingredients_description;

                }
            }
            
            



                
            
        }
    }
}
