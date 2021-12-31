using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Labyrinth : MonoBehaviour
{
    public GameObject secondPiece;
    public GameObject kvothe;
    public GameObject statusMinigame;
    public AudioManager audioManager;
    

    private int index;
    private float waitTime = 0.5f;
    private bool saved = false;

    // Update is called once per frame
    void Update()
    {
        // Has the transform changed since the last time the flag was set to 'false'?
        if (this.transform.hasChanged)
        {
            // SET rotation of arrows = -rotation of the tiles
            rotateArrows(this.gameObject);
            this.transform.hasChanged = false;
        }

        // Has the transform changed since the last time the flag was set to 'false' ?
        if (secondPiece.transform.hasChanged)
        {
            // SET rotation of arrows = -rotation of the tiles
            rotateArrows(secondPiece.gameObject);
            secondPiece.transform.hasChanged = false;
        }

        if (index == 0) //disabled
        {
            if (isOn(this.gameObject)) //Kvothe is on this map piece
            {
                //shows left arrow on piece 1
                left(this.transform.GetChild(0));
                //shows left arrow on piece 2
                left(secondPiece.transform.GetChild(0));

                index++;
                saved = false;
                playLabMusic();
                Debug.Log(index);
            }
        }
        else if (index == 1) //active - start condition
        {
            if (saved)
            {
                //shows left arrow on piece 1
                left(this.transform.GetChild(0));
                //shows left arrow on piece 2
                left(secondPiece.transform.GetChild(0));

                playLabMusic();
                saved = false;
            }
            // attraversa nel modo sbagliato
            if (crossedDown(this.gameObject) || crossedRight(this.gameObject) || crossedUp(this.gameObject))
            {
                playNormMusic();
                resetArrows();
                index = 0;
                saved = false;
                statusMinigame.SetActive(false);
                Debug.Log("Reset");
            }
            else if (crossedLeft(this.gameObject) && !isToLeft(this.gameObject, secondPiece)) {
                playNormMusic();
                resetArrows();
                index = 0;
                saved = false;
                statusMinigame.SetActive(false);
                Debug.Log("Reset");
            }
            else if (crossedLeft(this.gameObject) && isToLeft(this.gameObject, secondPiece))
            {
                //shows left arrow on piece 1
                left(this.transform.GetChild(0));
                //shows left arrow on piece 2
                left(secondPiece.transform.GetChild(0));

                //kvothe crossed left on the second piece
                index++;
                saved = false;
                status(index);
                Debug.Log(index);
            }

        }
        else if (index == 2) //
        {
            if (saved)
            {
                //shows left arrow on piece 1
                left(this.transform.GetChild(0));
                //shows left arrow on piece 2
                left(secondPiece.transform.GetChild(0));

                status(index);
                playLabMusic();
                saved = false;
            }
            // attraversa nel modo sbagliato
            if (crossedDown(secondPiece) || crossedRight(secondPiece) || crossedUp(secondPiece))
            {
                playNormMusic();
                resetArrows();
                index = 0;
                saved = false;
                statusMinigame.SetActive(false);
                Debug.Log("Reset");
            }
            else if (crossedLeft(secondPiece) && !isToLeft(secondPiece, this.gameObject))
            {
                playNormMusic();
                resetArrows();
                index = 0;
                saved = false;
                statusMinigame.SetActive(false);
                Debug.Log("Reset");
            }
            else if (crossedLeft(secondPiece) && isToLeft(secondPiece, this.gameObject))
            {
                //shows left arrow on piece 1
                down(this.transform.GetChild(0));
                //shows left arrow on piece 2
                left(secondPiece.transform.GetChild(0));

                //kvothe crossed left on the first piece
                index++;
                saved = false;
                status(index);
                Debug.Log(index);
            }
        }
        else if (index == 3) //
        {
            if (saved)
            {
                //shows left arrow on piece 1
                down(this.transform.GetChild(0));
                //shows left arrow on piece 2
                left(secondPiece.transform.GetChild(0));

                status(index);
                playLabMusic();
                saved = false;
            }
            // attraversa nel modo sbagliato
            if (crossedLeft(this.gameObject) || crossedRight(this.gameObject) || crossedUp(this.gameObject))
            {
                playNormMusic();
                resetArrows();
                index = 0;
                saved = false;
                statusMinigame.SetActive(false);
                Debug.Log("Reset");
            }
            else if (crossedDown(this.gameObject) && !isDown(this.gameObject, secondPiece))
            {
                playNormMusic();
                resetArrows();
                index = 0;
                saved = false;
                statusMinigame.SetActive(false);
                Debug.Log("Reset");
            }
            else if (crossedDown(this.gameObject) && isDown(this.gameObject, secondPiece))
            {
                //shows left arrow on piece 1
                down(this.transform.GetChild(0));
                //shows left arrow on piece 2
                down(secondPiece.transform.GetChild(0));

                //kvothe crossed down on the second piece
                index++;
                saved = false;
                status(index);
                Debug.Log(index);
            }

        }
        else if (index == 4) //
        {
            if (saved)
            {
                //shows left arrow on piece 1
                down(this.transform.GetChild(0));
                //shows left arrow on piece 2
                down(secondPiece.transform.GetChild(0));

                status(index);
                playLabMusic();
                saved = false;
            }
            // attraversa nel modo sbagliato
            if (crossedLeft(secondPiece) || crossedRight(secondPiece) || crossedUp(secondPiece))
            {
                playNormMusic();
                resetArrows();
                index = 0;
                saved = false;
                statusMinigame.SetActive(false);
                Debug.Log("Reset");
            }
            else if (crossedDown(secondPiece) && !isDown(secondPiece, this.gameObject))
            {
                playNormMusic();
                resetArrows();
                index = 0;
                saved = false;
                statusMinigame.SetActive(false);
                Debug.Log("Reset");
            }
            else if (crossedDown(secondPiece) && isDown(secondPiece, this.gameObject))
            {
                //shows left arrow on piece 1
                up(this.transform.GetChild(0));
                //shows left arrow on piece 2
                down(secondPiece.transform.GetChild(0));

                //kvothe crossed down on the first piece
                index++;
                saved = false;
                status(index);
                Debug.Log(index);
            }

        }
        else if (index == 5) //
        {
            if (saved)
            {
                //shows left arrow on piece 1
                up(this.transform.GetChild(0));
                //shows left arrow on piece 2
                down(secondPiece.transform.GetChild(0));

                status(index);
                playLabMusic();
                saved = false;
            }
            // attraversa nel modo sbagliato
            if (crossedLeft(this.gameObject) || crossedRight(this.gameObject) || crossedDown(this.gameObject))
            {
                playNormMusic();
                resetArrows();
                index = 0;
                saved = false;
                statusMinigame.SetActive(false);
                Debug.Log("Reset");
            }
            else if (crossedUp(this.gameObject) && !isUp(this.gameObject, secondPiece))
            {
                playNormMusic();
                resetArrows();
                index = 0;
                saved = false;
                statusMinigame.SetActive(false);
                Debug.Log("Reset");
            }
            else if (crossedUp(this.gameObject) && isUp(this.gameObject, secondPiece))
            {
                //do not show anymore arrows on piece 1
                this.transform.GetChild(0).gameObject.SetActive(false);
                //do not show anymore arrows on piece 2
                secondPiece.transform.GetChild(0).gameObject.SetActive(false);
                //kvothe crossed up on the second piece
                index++;
                saved = false;
                status(index);
                Debug.Log(index);
            }

        }
        else if (index == 6) //completed
        {
            if (saved)
            {
                //do not show anymore arrows on piece 1
                this.transform.GetChild(0).gameObject.SetActive(false);
                //do not show anymore arrows on piece 2
                secondPiece.transform.GetChild(0).gameObject.SetActive(false);
                
                status(index);
                playLabMusic();
                saved = false;
            }
            if(waitTime <= 0)
            {
                index++;
                saved = false;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        else if (index == 7)
        {
            statusMinigame.SetActive(false);
            secondPiece.transform.GetChild(1).gameObject.SetActive(true);

            playNormMusic();
            this.enabled = false;
            Debug.Log("Mission completed - script disabled");
        }

    }

    private bool crossedUp(GameObject tile)
    {
        return (kvothe.transform.position.y > tile.transform.position.y + 10.5);
    }
    private bool crossedLeft(GameObject tile)
    {
        return (kvothe.transform.position.x < tile.transform.position.x - 9);
    }
    private bool crossedRight(GameObject tile)
    {
        return (kvothe.transform.position.x > tile.transform.position.x + 9);
    }
    private bool crossedDown(GameObject tile)
    {
        return (kvothe.transform.position.y < tile.transform.position.y - 8);
    }


    private bool isToLeft(GameObject sourceTile, GameObject destTile)
    {
        return (destTile.transform.position.x + 18 == sourceTile.transform.position.x &&
                destTile.transform.position.y == sourceTile.transform.position.y);
    }
    private bool isToRight(GameObject sourceTile, GameObject destTile)
    {
        return (destTile.transform.position.x - 18 == sourceTile.transform.position.x &&
                destTile.transform.position.y == sourceTile.transform.position.y);
    }
    private bool isUp(GameObject sourceTile, GameObject destTile)
    {
        return (destTile.transform.position.x == sourceTile.transform.position.x &&
                destTile.transform.position.y - 18 == sourceTile.transform.position.y);
    }
    private bool isDown(GameObject sourceTile, GameObject destTile)
    {
        return (destTile.transform.position.x == sourceTile.transform.position.x &&
                destTile.transform.position.y + 18 == sourceTile.transform.position.y);
    }

    private bool isOn(GameObject tile)
    {
        return ((kvothe.transform.position.x < tile.transform.position.x + 9) &&
                (kvothe.transform.position.x > tile.transform.position.x - 9) &&
                (kvothe.transform.position.y < tile.transform.position.y + 9) &&
                (kvothe.transform.position.y > tile.transform.position.y - 9));
    }

    private void rotateArrows(GameObject tile)
    {
        tile.transform.Find("Arrows").transform.rotation = Quaternion.Euler(0, 0, -tile.transform.rotation.z);
    }

    private void left (Transform arrows)
    {
        arrows.transform.GetChild(1).gameObject.SetActive(true); //left
        arrows.transform.GetChild(0).gameObject.SetActive(false); //up
        arrows.transform.GetChild(2).gameObject.SetActive(false); //right
        arrows.transform.GetChild(3).gameObject.SetActive(false); //down
    }
    private void right(Transform arrows)
    {
        arrows.transform.GetChild(1).gameObject.SetActive(false); //left
        arrows.transform.GetChild(0).gameObject.SetActive(false); //up
        arrows.transform.GetChild(2).gameObject.SetActive(true); //right
        arrows.transform.GetChild(3).gameObject.SetActive(false); //down
    }
    private void down(Transform arrows)
    {
        arrows.transform.GetChild(1).gameObject.SetActive(false); //left
        arrows.transform.GetChild(0).gameObject.SetActive(false); //up
        arrows.transform.GetChild(2).gameObject.SetActive(false); //right
        arrows.transform.GetChild(3).gameObject.SetActive(true); //down
    }
    private void up(Transform arrows)
    {
        arrows.transform.GetChild(1).gameObject.SetActive(false); //left
        arrows.transform.GetChild(0).gameObject.SetActive(true); //up
        arrows.transform.GetChild(2).gameObject.SetActive(false); //right
        arrows.transform.GetChild(3).gameObject.SetActive(false); //down
    }

    private void resetArrows()
    {
        //shows left arrow on piece 1
        left(this.transform.GetChild(0));
        //shows left arrow on piece 2
        left(secondPiece.transform.GetChild(0));
    }

    private void status(int status)
    {
        if (!statusMinigame.activeSelf)
        {
            statusMinigame.SetActive(true);
        }

        if(status == 2)
        {
            statusMinigame.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            statusMinigame.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            statusMinigame.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
            statusMinigame.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
            statusMinigame.transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
        }
        else if (status == 3)
        {
            statusMinigame.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            statusMinigame.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            statusMinigame.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
            statusMinigame.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
            statusMinigame.transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
        }
        else if(status == 4)
        {
            statusMinigame.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            statusMinigame.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            statusMinigame.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
            statusMinigame.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
            statusMinigame.transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
        }
        else if (status == 5)
        {
            statusMinigame.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            statusMinigame.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            statusMinigame.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
            statusMinigame.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
            statusMinigame.transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
        }
        else if (status == 6)
        {
            statusMinigame.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            statusMinigame.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            statusMinigame.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
            statusMinigame.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
            statusMinigame.transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
        }
    }

    private void playLabMusic()
    {
        AudioSource bgrSrc = audioManager.GetSound("Background").source;
        AudioSource labSrc = audioManager.GetSound("Labyrinth").source;
        bgrSrc.Stop();
        labSrc.Play();
    }
    private void playNormMusic()
    {
        AudioSource bgrSrc = audioManager.GetSound("Background").source;
        AudioSource labSrc = audioManager.GetSound("Labyrinth").source;
        labSrc.Stop();
        bgrSrc.Play();
    }

    public int getIndex()
    {
        return index;
    }

    public void setIndex(int value)
    {
        saved = true;
        index = value;
        Debug.Log("Restoring the value ..." + value);
    }
}
