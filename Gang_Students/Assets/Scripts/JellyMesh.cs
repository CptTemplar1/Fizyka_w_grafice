using UnityEngine;

/// <summary>
/// Symuluje efekt elastycznoœci siatki obiektu, nadaj¹c jej w³aœciwoœci podobne do galarety.
/// </summary>
/// <remarks>
/// Klasa JellyMesh modyfikuje siatkê mesh obiektu, aby zachowywa³a siê jak galaretka przy interakcjach.
/// Umo¿liwia to dynamiczne zmiany kszta³tu obiektu w czasie rzeczywistym.
/// </remarks>
public class JellyMesh : MonoBehaviour
{
    public float Intensity; ///< Intensywnoœæ efektu galarety.
    public float Mass; ///< Masa punktów mesh, wp³ywaj¹ca na dynamikê ruchu.
    public float stiffness; ///< Sztywnoœæ mesh, wp³ywaj¹ca na opór zmiany kszta³tu.
    public float damping; ///< T³umienie ruchów mesh, wp³ywaj¹ce na szybkoœæ powrotu do pierwotnego kszta³tu.

    private Mesh OriginalMesh, MeshClone; ///< Oryginalna siatka mesh i jej klon.
    private MeshRenderer mRenderer; ///< Renderer siatki mesh.
    private JellyVertex[] jv; ///< Tablica wierzcho³ków do symulacji efektu galarety.
    private Vector3[] vertexArray; ///< Tablica pozycji wierzcho³ków mesh.

    /// <summary>
    /// Inicjalizuje konfiguracjê mesh i przygotowuje dane do symulacji elastycznoœci.
    /// </summary>
    void Start()
    {
        OriginalMesh = GetComponent<MeshFilter>().sharedMesh;
        MeshClone = Instantiate(OriginalMesh);
        GetComponent<MeshFilter>().sharedMesh = MeshClone;
        mRenderer = GetComponent<MeshRenderer>();
        jv = new JellyVertex[MeshClone.vertices.Length];
        for (int i = 0; i < MeshClone.vertices.Length; i++)
        {
            jv[i] = new JellyVertex(i, transform.TransformPoint(MeshClone.vertices[i]));
        }
    }

    /// <summary>
    /// Aktualizuje siatkê mesh w ka¿dym cyklu fizyki, symuluj¹c elastycznoœæ.
    /// </summary>
    void FixedUpdate()
    {
        vertexArray = OriginalMesh.vertices;
        for (int i = 0; i < jv.Length; i++)
        {
            Vector3 target = transform.TransformPoint(vertexArray[jv[i].ID]);
            float intensity = (1 - (mRenderer.bounds.max.y - target.y) / mRenderer.bounds.size.y) * Intensity;
            jv[i].Shake(target, Mass, stiffness, damping);
            target = transform.InverseTransformPoint(jv[i].Position);
            vertexArray[jv[i].ID] = Vector3.Lerp(vertexArray[jv[i].ID], target, intensity);
        }
        MeshClone.vertices = vertexArray;
    }

    /// <summary>
    /// Reprezentuje wierzcho³ek siatki mesh, który jest modyfikowany do stworzenia efektu elastycznoœci.
    /// </summary>
    public class JellyVertex
    {
        public int ID; ///< ID wierzcho³ka.
        public Vector3 Position; ///< Aktualna pozycja wierzcho³ka.
        public Vector3 velocity, Force; ///< Prêdkoœæ i si³a dzia³aj¹ca na wierzcho³ek.

        /// <summary>
        /// Konstruktor inicjalizuj¹cy wierzcho³ek.
        /// </summary>
        /// <param name="_id">ID wierzcho³ka.</param>
        /// <param name="_pos">Pocz¹tkowa pozycja wierzcho³ka.</param>
        public JellyVertex(int _id, Vector3 _pos)
        {
            ID = _id;
            Position = _pos;
        }

        /// <summary>
        /// Symuluje ruch elastyczny wierzcho³ka.
        /// </summary>
        /// <param name="target">Docelowa pozycja wierzcho³ka.</param>
        /// <param name="m">Masa wierzcho³ka.</param>
        /// <param name="s">Sztywnoœæ siatki.</param>
        /// <param name="d">T³umienie ruchu.</param>
        public void Shake(Vector3 target, float m, float s, float d)
        {
            Force = (target - Position) * s;
            velocity = (velocity + Force / m) * d;
            Position += velocity;
            if ((velocity + Force + Force / m).magnitude < 0.001f)
                Position = target;
        }

    }

}