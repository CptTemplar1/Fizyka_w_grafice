using UnityEditor;
using UnityEngine;

///Klasa służaca do wygenerowania tekstury 3D szumu przy pomocy ComputeShadera
public class NoiseGenerator : MonoBehaviour
{
    /// Referencja do Compute Shadera odpowiedzialnego za generowanie szumu
    public ComputeShader computeShader;
    /// Bufor używany do przekazywania danych pomiędzy GPU a CPU
    ComputeBuffer buffer;
    /// Materiał używany do renderowania efektu mgły
    public Material fogMat;
    ///Szerokość generowanej tekstury
    public int size = 32;
    ///Wysokość generowanej tekstury
    public int height = 16;
    /// Referencja do tekstury 3D
    static Texture3D tex3D;

    /// Metoda wykonująca się w każdej klatce. Wykorzystana do obsługi generowania nowej tekstury szumu po kliknięciu Spacji.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Sprawdza, czy został naciśnięty klawisz Spacji
        {
            CreateTexture(); // Wywołuje funkcję tworzącą teksturę
        }
    }

    /// Parametry określające wygląd tekstury szumu
    public float noiseSize = 1, seed = 0;

    [ContextMenu("Generate Noise")] // Pozwala na wywołanie tej funkcji z menu kontekstowego w edytorze Unity
    ///Metoda tworząca teksturę 3D szumu
    void CreateTexture()
    {
        if (tex3D == null) // Sprawdza, czy tekstura 3D nie została jeszcze utworzona
        {
            tex3D = new Texture3D(size, height, size, TextureFormat.RFloat, false); // Tworzy nową teksturę 3D
        }

        fogMat.SetTexture("_Noise", tex3D); // Ustawia teksturę 3D jako parametr materiału efektu mgły
        int pixels = size * size * height; // Oblicza ilość pikseli w teksturze
        ComputeBuffer buffer = new ComputeBuffer(pixels, sizeof(float)); // Tworzy nowy bufor danych

        computeShader.SetBuffer(0, "Result", buffer); // Ustawia bufor jako parametr w Compute Shaderze
        computeShader.SetFloat("size", size); // Ustawia rozmiar jako parametr w Compute Shaderze
        computeShader.SetFloat("height", height); // Ustawia wysokość jako parametr w Compute Shaderze
        computeShader.SetFloat("seed", seed); // Ustawia ziarno jako parametr w Compute Shaderze
        computeShader.SetFloat("noiseSize", noiseSize); // Ustawia rozmiar szumu jako parametr w Compute Shaderze
        computeShader.Dispatch(0, size / 8, height / 8, size / 8); // Wywołuje obliczenia w Compute Shaderze

        float[] noise = new float[pixels]; // Tablica przechowująca wartości szumu
        Color[] colors = new Color[pixels]; // Tablica przechowująca kolory
        buffer.GetData(noise); // Pobiera dane z bufora
        buffer.Release(); // Zwalnia bufor

        for (int i = 0; i < pixels; i++) // Przetwarza wartości szumu na kolory
        {
            colors[i] = new Color(noise[i], 0, 0, 0);
        }
        tex3D.SetPixels(colors); // Ustawia kolory na teksturze 3D
        tex3D.Apply(); // Zastosowuje zmiany w teksturze

    }


    [ContextMenu("Save Noise")] // Pozwala na wywołanie tej funkcji z menu kontekstowego w edytorze Unity

    ///Funkcja zapisująca wygenerowaną teksturę do projektu Unity w określonej ścieżce
    void CreateTexture3D()
    {
#if UNITY_EDITOR
        AssetDatabase.CreateAsset(tex3D, "Assets/Shaders/FogShader/3DTextureNoise.asset");
#endif
    }
}

