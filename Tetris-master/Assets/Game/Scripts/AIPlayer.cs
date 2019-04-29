using EmbASP4Unity.it.unical.mat.objectsMapper.ActuatorsScripts;
using EmbASP4Unity.it.unical.mat.objectsMapper.BrainsScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class AIPlayer : MonoBehaviour
    {
        public Vector3 start;
        public Vector3 end;
        private Vector3 previousStart;
        private Vector3 prevoiusEnd;
        int numOfMove=-2;
        int numOfLateralMove;
        int numOfRotation;
        string typeOfLateralMove = "";//left, right
        int oldTetromino=-1;
        bool tetrominoChanged;
        int currentTetromino;
        int aiTetromino;
        public bool moveDone;
        Brain brain;
        TetrominoSpawner spawner;

        void Start()
        {
            moveDone = true;
            start = Vector3.zero;
            end = Vector3.zero;
            brain = GameObject.FindObjectOfType<Brain>();
            spawner = GameObject.FindObjectOfType<TetrominoSpawner>();
            InvokeRepeating("move",4,1f);
            
            InvokeRepeating("updateTetromino", 0, 0.2f);
        }

        public void updateTetromino()
        {
            // Debug.Log("updating tetromino");
            if (spawner.spawned)
            {
                currentTetromino = spawner.lastInstantiated;
                tetrominoChanged=true;
                spawner.spawned = false;
            }
            
        }

        public void move()
        {
            
            
            //Debug.Log("trying to move");

            if (brain.areActuatorsReady() &&   tetrominoChanged)
            {
                //Debug.Log("updating actuators");
                moveDone = true;
                foreach (SimpleActuator act in brain.getActuators())
                {
                    act.UpdateProperties();
                }
                brain.setActuatorsReady(false);
                start = Vector3.zero;
                end = Vector3.zero;
                if (currentTetromino == aiTetromino)
                {
                    tetrominoChanged = false;
                }
            }
            
            if (currentTetromino == aiTetromino && numOfMove > 0 && moveDone)
            {
                
                moveDone = false;
                Debug.Log("I have to do something");
                numOfMove--;
                if (numOfRotation > 0)
                {
                    
                    Debug.Log("Rotating");
                    numOfRotation--;
                    start = new Vector3(0, 0, 0);
                    end = new Vector3(0, 100, 0);
                    previousStart = new Vector3(0, 0, 0);
                    prevoiusEnd = new Vector3(0, 100, 0); 

                }
                else
                {
                    numOfLateralMove--;
                    if (typeOfLateralMove.Equals("left"))
                    {
                        Debug.Log("Moving left ");
                        start = new Vector3(100, 0, 0);
                        end = new Vector3(0, 0, 0);
                        previousStart = new Vector3(100, 0, 0);
                        prevoiusEnd = new Vector3(0, 0, 0);
                    }
                    else
                    {
                        Debug.Log("Moving right ");
                        start = new Vector3(0, 0, 0);
                        end = new Vector3(100, 0, 0);
                        previousStart = new Vector3(0, 0, 0);
                        prevoiusEnd = new Vector3(100, 0, 0);
                    }
                }
                
            }
            else if (numOfMove == 0)
            {
                Debug.Log("Going down ");
                start = new Vector3(0, 100, 0);
                end = new Vector3(0, 0, 0);
                numOfMove = -1;
            }
            else if(!moveDone && (previousStart!=Vector3.zero || prevoiusEnd!=Vector3.zero))
            {
                Debug.Log("resetting");
                start = previousStart;
                end = prevoiusEnd;
            }else if (currentTetromino != aiTetromino)
            {
                Debug.Log("tetr "+currentTetromino+" "+aiTetromino);
                numOfMove = -1;

            }
            else
            {
                Debug.Log("num of move "+numOfMove+" move done:"+moveDone);
            }
                
        }
    }
}
