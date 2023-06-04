﻿using BepInEx;
using R2API;
using RoR2;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace CaptainSkyboxPlugin
{
    [BepInDependency(SceneAssetAPI.PluginGUID)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]

    public class CaptainSkyboxPlugin : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "swuff";
        public const string PluginName = "SafeTravelsInSkybox";
        public const string PluginVersion = "1.0.0";

        private string sceneName = "moon";

        //to-do: find a way to make airstrike starting angle always match roughly where ship is?
        //maybe close to impossible, need to look into how those work. would be neat.

        public void Start()
        {
            SceneAssetAPI.AddAssetRequest(sceneName, OnMoonSceneLoaded);
            CharacterBody.onBodyStartGlobal += BodyStartGlobal;
        }

        private void OnMoonSceneLoaded(GameObject[] rootObjects)
        {
            GameObject esc = rootObjects[0];
            if (esc != null)
            {
                Transform eso = esc.transform.Find("EscapeSequenceObjects");
                if (eso != null)
                {
                    Transform csh = eso.transform.Find("ColonyShipHolder");
                    if (csh != null)
                    {
                        ColonyShipManager shipManager = FindObjectOfType<ColonyShipManager>();
                        if (shipManager == null)
                        {
                            GameObject managerObject = new GameObject("ColonyShipManager");
                            shipManager = managerObject.AddComponent<ColonyShipManager>();
                        }
                        shipManager.SetColonyShip(csh.gameObject);
                    }
                }
            }
        }

        public void BodyStartGlobal(CharacterBody body)
        {
            if (!NetworkServer.active)
                return;

            if (body.baseNameToken == "CAPTAIN_BODY_NAME")
            {
                Debug.Log("SafeTravelsInSkybox : Good morning, Captain.");
                GameObject colonyShip = GameObject.Find("ColonyShipHolderStage(Clone)");
                if (colonyShip == null)
                {
                    Debug.Log("SafeTravelsInSkybox : Deploying UES Safe Travels to " + SceneManager.GetActiveScene().name + " . . .");

                    ColonyShipManager shipManager = FindObjectOfType<ColonyShipManager>();

                    if (shipManager != null)
                    {
                        var position = Vector3.zero;
                        var rotation = Quaternion.Euler(-90, 0, 0);
                        var currScene = SceneManager.GetActiveScene().name;

                        //to-do: this should use a library of some sort instead of double switch statements but cba atm
                        //could also probably also just define these in a better order to be one switch statement
                        //again, cba

                        switch (currScene)
                        {
                            case "golemplains":
                                position = new Vector3(1093f, 2348f, 1487f);
                                rotation = Quaternion.Euler(354.442f, 98.19998f, 78.091f);
                                break;
                            case "golemplains2":
                                position = new Vector3(1093f, 2348f, 1487f);
                                rotation = Quaternion.Euler(354.442f, 98.19998f, 78.091f);
                                break;
                            case "blackbeach":
                                position = new Vector3(1093f, 1748f, 887f);
                                rotation = Quaternion.Euler(0f, 90f, 90f);
                                break;
                            case "blackbeach2":
                                position = new Vector3(500f, 1848f, 1287f);
                                rotation = Quaternion.Euler(354.442f, 98.19998f, 78.091f);
                                break;
                            case "goolake":
                                position = new Vector3(2093f, 2348f, 1487f);
                                rotation = Quaternion.Euler(354.442f, 98.19998f, 78.091f);
                                break;
                            case "foggyswamp":
                                position = new Vector3(1000f, 1300f, -900f);
                                rotation = Quaternion.Euler(354.442f, 115f, 90f);
                                break;
                            case "frozenwall":
                                position = new Vector3(-2000f, 1800f, -400f);
                                rotation = Quaternion.Euler(354.442f, 200f, 90f);
                                break;
                            case "wispgraveyard":
                                position = new Vector3(1293f, 1648f, -1487f);
                                rotation = Quaternion.Euler(354.442f, 80f, 78.091f);
                                break;
                            /*case "dampcavesimple": //just nowhere to put it here that makes sense
                                position = new Vector3(2093f, 2348f, 1487f);
                                rotation = Quaternion.Euler(354.442f, 98.19998f, 78.091f);
                                break;*/
                            case "shipgraveyard":
                                position = new Vector3(0f, 1000f, -900f);
                                rotation = Quaternion.Euler(354.442f, 120f, 78.091f);
                                break;
                            case "rootjungle":
                                position = new Vector3(400f, 2348f, 0f);
                                rotation = Quaternion.Euler(354.442f, 98.19998f, 78.091f);
                                break;
                            case "skymeadow":
                                position = new Vector3(1600f, 1800f, 0f);
                                rotation = Quaternion.Euler(354.442f, 140f, 78.091f);
                                break;
                            case "snowyforest":
                                position = new Vector3(1300f, 1100f, 300f);
                                rotation = Quaternion.Euler(0f, 90f, 90f);
                                break;
                            case "ancientloft":
                                position = new Vector3(600f, 1800f, 1487f);
                                rotation = Quaternion.Euler(354.442f, 98.19998f, 78.091f);
                                break;
                            case "sulfurpools":
                                position = new Vector3(2093f, 2348f, 1487f);
                                rotation = Quaternion.Euler(354.442f, 330f, 290f);
                                break;
                            case "moon2":
                                position = new Vector3(2093f, 2348f, 1487f);
                                rotation = Quaternion.Euler(354.442f, -4.2f, 290f);
                                break;
                            case "FBLScene":
                                position = new Vector3(300f, 2348f, 2400f);
                                rotation = Quaternion.Euler(354.442f, 240f, 70f);
                                break;
                            case "slumberingsatellite":
                                position = new Vector3(2093f, 2348f, 1487f);
                                rotation = Quaternion.Euler(354.442f, 310f, 290f);
                                break;
                            case "drybasin":
                                position = new Vector3(600f, 1400f, 800f);
                                rotation = Quaternion.Euler(354.442f, 355f, 290f);
                                break;
                        }

                        var shipScenePrefab = Instantiate(shipManager.ColonyShip, position, rotation);
                        shipScenePrefab.SetActive(true);

                        switch (currScene)
                        {
                            case "golemplains":
                                shipScenePrefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                                break;
                            case "golemplains2":
                                shipScenePrefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                                break;
                            case "blackbeach":
                                shipScenePrefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                                break;
                            case "blackbeach2":
                                shipScenePrefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                                break;
                            case "goolake":
                                shipScenePrefab.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                                break;
                            case "foggyswamp":
                                shipScenePrefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                                break;
                            case "frozenwall":
                                shipScenePrefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                                break;
                            case "wispgraveyard":
                                shipScenePrefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                                break;
                            /*case "dampcavesimple":
                                shipScenePrefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                                break;*/
                            case "shipgraveyard":
                                shipScenePrefab.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                                break;
                            case "rootjungle":
                                shipScenePrefab.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                                break;
                            case "skymeadow":
                                shipScenePrefab.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
                                break;
                            case "snowyforest":
                                shipScenePrefab.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                                break;
                            case "ancientloft":
                                shipScenePrefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                                break;
                            case "sulfurpools":
                                shipScenePrefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                                break;
                            case "moon2":
                                shipScenePrefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                                break;
                            case "FBLScene":
                                shipScenePrefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                                break;
                            case "slumberingsatellite":
                                shipScenePrefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                                break;
                            case "drybasin":
                                shipScenePrefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                                break;
                        }
                    }
                }
                else
                    Debug.Log("SafeTravelsInSkybox : UES Safe Travels already deployed.");
            }
        }
    }

    public class ColonyShipManager : MonoBehaviour
    {
        public static ColonyShipManager Instance { get; private set; }
        public GameObject ColonyShip { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void SetColonyShip(GameObject ship)
        {
            ColonyShip = Instantiate(ship);
            if (ColonyShip == null)
                Debug.Log("failed to instantiate colonyship clone; please send a log to swuff★#2224 :(");
            else
            {
                ColonyShip.name = ship.name + "Stage";
                ColonyShip.SetActive(false);
                DontDestroyOnLoad(ColonyShip);
                Debug.Log("SafeTravelsInSkybox : Preparing UES Safe Travels . . .");
            }
        }
    }
}