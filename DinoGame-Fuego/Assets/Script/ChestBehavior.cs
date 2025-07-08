using UnityEngine;
using TMPro;

public class ChestBehaviour : MonoBehaviour
{
    public GameObject coinPrefab; // Prefab untuk Coin
    public int coinAmount = 10; // Jumlah Coin yang muncul
    public TextMeshProUGUI interactText; // UI teks untuk interaksi
    public Transform coinSpawnPoint; // Tempat Coin muncul di dunia

    private bool isPlayerNearby = false; // Apakah Player dekat dengan Chest
    private bool isOpened = false; // Apakah Chest sudah dibuka

    void Start()
    {
        interactText.gameObject.SetActive(false); // Sembunyikan teks interaksi
    }

    void Update()
    {
        if (isPlayerNearby && !isOpened && Input.GetKeyDown(KeyCode.Space))
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        isOpened = true; // Tandai Chest sudah dibuka
        interactText.gameObject.SetActive(false); // Sembunyikan teks interaksi

        // Munculkan Coin
        GameObject coin = Instantiate(coinPrefab, coinSpawnPoint.position, Quaternion.identity);

        // Atur jumlah Coin ke CoinBehavior
        CoinChestBehavior coinBehavior = coin.GetComponent<CoinChestBehavior>();
        if (coinBehavior != null)
        {
            coinBehavior.coinAmount = coinAmount; // Atur jumlah Coin sesuai dengan Chest
        }

        // Tambahkan teks jumlah Coin di atas Coin
        TextMeshPro coinText = coin.GetComponentInChildren<TextMeshPro>(); // Cari komponen TextMeshPro
        if (coinText != null)
        {
            coinText.text = "x " + coinAmount; // Atur teks sesuai jumlah Coin
        }
        else
        {
            Debug.LogWarning("TextMeshPro untuk jumlah Coin tidak ditemukan di prefab Coin!");
        }

        // Hapus Chest setelah dibuka
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true; // Tandai Player dekat
            interactText.gameObject.SetActive(true); // Tampilkan teks interaksi
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false; // Player menjauh
            interactText.gameObject.SetActive(false); // Sembunyikan teks interaksi
        }
    }
}