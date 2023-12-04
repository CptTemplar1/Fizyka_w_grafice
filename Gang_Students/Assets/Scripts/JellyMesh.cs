using UnityEngine;

/// <summary>
/// Symuluje efekt elastyczno�ci siatki obiektu, nadaj�c jej w�a�ciwo�ci podobne do galarety.
/// </summary>
/// <remarks>
/// Klasa JellyMesh modyfikuje siatk� mesh obiektu, aby zachowywa�a si� jak galaretka przy interakcjach.
/// Umo�liwia to dynamiczne zmiany kszta�tu obiektu w czasie rzeczywistym.
/// </remarks>
public class JellyMesh : MonoBehaviour
{
    public float Intensity; ///< Intensywno�� efektu galarety.
    public float Mass; ///< Masa punkt�w mesh, wp�ywaj�ca na dynamik� ruchu.
    public float stiffness; ///< Sztywno�� mesh, wp�ywaj�ca na op�r zmiany kszta�tu.
    public float damping; ///< T�umienie ruch�w mesh, wp�ywaj�ce na szybko�� powrotu do pierwotnego kszta�tu.

    private Mesh OriginalMesh, MeshClone; ///< Oryginalna siatka mesh i jej klon.
    private MeshRenderer mRenderer; ///< Renderer siatki mesh.
    private JellyVertex[] jv; ///< Tablica wierzcho�k�w do symulacji efektu galarety.
    private Vector3[] vertexArray; ///< Tablica pozycji wierzcho�k�w mesh.

    /// <summary>
    /// Inicjalizuje konfiguracj� mesh i przygotowuje dane do symulacji elastyczno�ci.
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
    /// Aktualizuje siatk� mesh w ka�dym cyklu fizyki, symuluj�c elastyczno��.
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
    /// Reprezentuje wierzcho�ek siatki mesh, kt�ry jest modyfikowany do stworzenia efektu elastyczno�ci.
    /// </summary>
    public class JellyVertex
    {
        public int ID; ///< ID wierzcho�ka.
        public Vector3 Position; ///< Aktualna pozycja wierzcho�ka.
        public Vector3 velocity, Force; ///< Pr�dko�� i si�a dzia�aj�ca na wierzcho�ek.

        /// <summary>
        /// Konstruktor inicjalizuj�cy wierzcho�ek.
        /// </summary>
        /// <param name="_id">ID wierzcho�ka.</param>
        /// <param name="_pos">Pocz�tkowa pozycja wierzcho�ka.</param>
        public JellyVertex(int _id, Vector3 _pos)
        {
            ID = _id;
            Position = _pos;
        }

        /// <summary>
        /// Symuluje ruch elastyczny wierzcho�ka.
        /// </summary>
        /// <param name="target">Docelowa pozycja wierzcho�ka.</param>
        /// <param name="m">Masa wierzcho�ka.</param>
        /// <param name="s">Sztywno�� siatki.</param>
        /// <param name="d">T�umienie ruchu.</param>
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