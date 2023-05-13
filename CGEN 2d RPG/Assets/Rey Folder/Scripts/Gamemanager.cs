using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Gamemanager : MonoBehaviour
{

    [SerializeField] private Transform gameTransform;
    [SerializeField] private Transform piecePreFab;
    [SerializeField] private GameObject panel;

    private List<Transform> pieces;
    private int emptyLocation;
    private int size;
    private bool shuffling = false;
    private bool gameComplete = false;


    private void CreateGamePieces(float gapThickness)
    {
        float width = 1 / (float)size;
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                Transform piece = Instantiate(piecePreFab, gameTransform);
                pieces.Add(piece);
                piece.localPosition = new Vector3(-1 + (2 * width * col) + width,
                                                   +1 - (2 * width * row) - width,
                                                   0);
                piece.localScale = ((2 * width) - gapThickness) * Vector3.one;
                piece.name = $"{(row * size) + col}";
                if ((row == size - 1) && (col == size - 1))
                {
                    emptyLocation = (size * size) - 1;
                    piece.gameObject.SetActive(false);
                }
                else
                {
                    float gap = gapThickness / 2;
                    Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                    Vector2[] uv = new Vector2[4];
                    uv[0] = new Vector2((width * col) + gap, 1 - ((width * (row + 1)) - gap));
                    uv[1] = new Vector2((width * (col + 1)) - gap, 1 - ((width * (row + 1)) - gap));
                    uv[2] = new Vector2((width * col) + gap, 1 - ((width * row) + gap));
                    uv[3] = new Vector2((width * (col + 1)) - gap, 1 - ((width * row) + gap));
                    mesh.uv = uv;
                }
            }
        }
    }
    public void LoadNewScene()
    {
        SceneManager.LoadScene("Spaceship");
    }


    void Start()
    {
        panel.SetActive(false);
        pieces = new List<Transform>();
        size = 2;
        CreateGamePieces(0.01f);
        shuffling = true;
        StartCoroutine(WaitShuffle(0.5f));

    }

    // Update is called once per frame
    void Update()
    {
        

        if (!shuffling && CheckCompletion())
        {
            shuffling = true;
            StartCoroutine(WaitShuffle(0.5f));

        }

        if (!gameComplete && Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                for (int i = 0; i < pieces.Count; i++)
                {
                    if (pieces[i] == hit.transform)
                    {

                        GetComponent<AudioSource>().Play();
                        if (SwapIfValid(i, -size, size)) { break; }
                        if (SwapIfValid(i, +size, size)) { break; }
                        if (SwapIfValid(i, -1, 0)) { break; }
                        if (SwapIfValid(i, +1, size - 1)) { break; }

                    }
                }
            }
        }
    }


    private bool SwapIfValid(int i, int offset, int colCheck){
        if (((i % size ) != colCheck) && ((i+ offset) == emptyLocation)){
            (pieces[i], pieces[i + offset]) = (pieces[i + offset], pieces[i]);
            (pieces[i].localPosition, pieces[i + offset].localPosition) = ((pieces[i + offset].localPosition, pieces[i].localPosition));
            emptyLocation = i;
            GetComponent<AudioSource>().Play();
            return true;
        }
        return false;
    }
    public int stageNumber;

    private bool CheckCompletion()
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i].name != $"{i}")
            {
                return false;
            }
        }

        panel.SetActive(true);

        StageManager.instance.CompleteCurrentStage(stageNumber);

        gameComplete = true;

        return true;
    }



    private IEnumerator WaitShuffle(float duration){
        yield return new WaitForSeconds(duration);
        Shuffle();
        shuffling = false;
    }

    private void Shuffle(){
        int count = 0;
        int last = 0;
        while (count<(size * size *size)){
            int rnd = Random.Range(0, size * size);
            if (rnd == last) { continue; }
            last = emptyLocation;
            if (SwapIfValid(rnd,-size,size)){
                count++;
            }else if (SwapIfValid(rnd, +size, size)){
                count++;
            }else if (SwapIfValid(rnd, -1, 0)){
                count++;
            }else if (SwapIfValid(rnd, +1, size - 1)){
                count++;
            }
        }
    }
}
