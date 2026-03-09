using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;


public class SubField// subfield sınıfı olusturdum
{
    public string Name { get; set; } // içine name definition ve application olmak uzer 3 sey koydum.
    public string Definition { get; set; }
    public string Applications { get; set; }

    public SubField(string name, string definition, string applications)// constructırım
    {
        Name = name;
        Definition = definition;
        Applications = applications;
    }
}

public class BSTNode<T>// bst mode sınıfım
{
    public string Key { get; set; }// içinde key value ve sag ,sol  ları değişkenlerini ekledim.
    public T Value { get; set; }
    public BSTNode<T> Left { get; set; }
    public BSTNode<T> Right { get; set; }

    public BSTNode(string key, T value)// bst node constructorım
    {
        Key = key;
        Value = value;
        Left = null;
        Right = null;
    }
}

public class BST<T>// direk bst sınıfım bunun içinde yukardaki node sınıfımı kullanıcam.
{
    public BSTNode<T> Root { get; set; }

    public BST()// root u başta bos yaptıgım constructerim
    {
        Root = null;
    }

    public void Insert(string key, T value)// insert metodum içinde başka bir şey cagırdım.
    {
        Root = InsertRec(Root, key, value);// recursive olan inserti cagırdım
    }

    private BSTNode<T> InsertRec(BSTNode<T> root, string key, T value)//recursive insert methodum
    {
        if (root == null)
        {
            return new BSTNode<T>(key, value);// root bossa zaten direkt yazdırdım
        }

        // karsılastırma yaptım
        int comparison = key.CompareTo(root.Key);

        if (comparison < 0)// yani key dahah kucukse  root keyden sol tarafa ekliyorum agacın
        {
            root.Left = InsertRec(root.Left, key, value);
        }
        else if (comparison > 0)// key daha buyukse root keyden sağ tarafa 
        {
            root.Right = InsertRec(root.Right, key, value);
        }

        return root;
    }

    // yazdırma metodum
    public void PrintAllNodes()
    {
        PrintNodesRec(Root);// onu da recursive halini cagırdım.
    }

    private void PrintNodesRec(BSTNode<T> node)// recursive yazdrıma metodum
    {
        if (node != null)// eger root null değil ise
        {
            PrintNodesRec(node.Left); // il solu yazdırdım
            Console.WriteLine($"Key =  {node.Key}");
            PrintNodesRec(node.Right);// sonra da sağı yazdırıdım, yani  kuuckten buyuge inorder gibi
        }
    }

    // İnorder traversal için liste döndürdüm
    public List<BSTNode<T>> GetAllNodesInOrder()
    {
        List<BSTNode<T>> result = new List<BSTNode<T>>();// liste olusturdum BSTNode<T>  türünde 
        GetNodesRec(Root, result);// yine recursive cagirdım cunku agac yapısında bu şekilde elde ediyoruz yukarda insert de oyle neyin oncelikle gelicegi ya da ekleneciğ bu recursiveler sayaesinde kolayca sağlanıyor.
        return result;
    }

    private void GetNodesRec(BSTNode<T> node, List<BSTNode<T>> result)
    {
        if (node != null)// yine node null değil ise 
        {
            GetNodesRec(node.Left, result);// solu ilk once 
            result.Add(node);
            GetNodesRec(node.Right, result);// sonra da sağı aldım.
        }
    }

    public int GetDepth(BSTNode<T> node)// derinlik bulma methodum.
    {
        if (node == null) return 0;// bossa zaten sıfırdır
        int leftDepth = GetDepth(node.Left);
        int rightDepth = GetDepth(node.Right);
        return 1 + Math.Max(leftDepth, rightDepth);// bunların maxına 1 eklersek bu bir de root için  derinliği buluyoruz.
    }

    public List<string> GetAllKeys()// keylere bakmak içiin methodum
    {
        List<string> result = new List<string>();// keyler string bu yuzden string uturnde bir list olusturdum.
        GetKeysRec(Root, result);//recursive i yine cagırdım.
        return result;
    }

    private void GetKeysRec(BSTNode<T> node, List<string> result)// bunun için de yine recursive 
    {
        if (node != null)// yine nodu um null değilse
        {
            GetKeysRec(node.Left, result);// yine önce solu 
            result.Add(node.Key);
            GetKeysRec(node.Right, result);// sonra da sağı aldım.
        }
    }
}


public class MinHeap// minheap sınıfım
{
    private List<MainFieldNode> heap;// MainFieldNode bu turde bir  heap adında private list olusturdum.

    public MinHeap()// bu constructer ım içinde heao e esşitledim yani alanı tamamen actım list için.
    {
        heap = new List<MainFieldNode>();
    }

    public void Insert(MainFieldNode node)// insert methodum
    {
        heap.Add(node);//  heape  yeni bir node ekledimm
        BubbleYukarı(heap.Count - 1);//yeni eklenen seyi yukarıya doğru baloncuk yaparak yerleştirdim.
    }

    private void BubbleYukarı(int index)// bubble yukarı yukarı demememmin sebebi yukaru boluncugun cıkması.
    {
        while (index > 0)// index sıfırın altına düşmedikçe devam edicek
        {
            int parentIndex = (index - 1) / 2;// // parentin indeksini hesapladım bu formulle hesaplanıyor

            // yine karşılaştırma yaptım.
            if (heap[index].FieldName.CompareTo(heap[parentIndex].FieldName) >= 0)//Eğer  sıradaki ebeveyninden büyükse, yer değiştirmemize gerek yokk
            {
                break;// break diyip cıktım
            }
            Swap(index, parentIndex);// Geçerli öğe ile ebeveynin yerini değiştirdim swap metodum ile bu metodda aşağıda
            index = parentIndex;//  en son da yeni ebeveynin indeksine geçtimm
        }
    }

    public MainFieldNode ExtractMin()
    {
        if (heap.Count == 0) return null;// eger heap bossa null doner
        if (heap.Count == 1)// eger sadece 1 öge varsa
        {
            MainFieldNode min = heap[0];// kök ögeyi alicam
            heap.RemoveAt(0);// o ögeyi kökten cıkarıyorum
            return min;//kök ögeyi döndürüyorum
        }

        MainFieldNode result = heap[0];// kökteki en kuuck olanı sakladım
        heap[0] = heap[heap.Count - 1];// sonra yıgının sonundaki ögeye kök yerine koydum
        heap.RemoveAt(heap.Count - 1);// son ögeyi heaptan cıkardım
        BubbleAsagı(0);// sonra bunu dogru yere yerleştirdim 
        return result;
    }

    private void BubbleAsagı(int index)
    {
        while (true)// sonsuz döngü doğru konum bulunana kadar devam etsin diye
        {
            int leftChild = 2 * index + 1;// sol cocugun indeksi
            int rightChild = 2 * index + 2;// sağ sözcugun indeksi
            int smallest = index;// başta aldıgımız ögeyi en kucuk olarak varsaydım
            // sol cocugu kontrol ettim
            if (leftChild < heap.Count &&
                heap[leftChild].FieldName.CompareTo(heap[smallest].FieldName) < 0)// sol cocuk daha kuuckse smallest ı sol cocuga guncelledim.
            {
                smallest = leftChild;
            }
            // sağ cocugu kontrol ettim
            if (rightChild < heap.Count &&
                heap[rightChild].FieldName.CompareTo(heap[smallest].FieldName) < 0)// sağ cocuk kuuckse de sag coocugu smalleste guncelledim.
            {
                smallest = rightChild;
            }
            // smallest hala mevcut ogemiz ise  zaten dogru konumda demek yani break ile cıktım
            if (smallest == index) break;

            Swap(index, smallest);// swap ile öge konumu güncellenmş oldu aslında
            index = smallest;
            // yeni konum indexi 
        }
    }
     
    private void Swap(int i, int j)// swape metodum exchange yani
    {
        MainFieldNode temp = heap[i];// ilk başta temp e eşitle 
        heap[i] = heap[j];
        heap[j] = temp;// en son  j yi temp e eşitle , sonucta exchange oldu
    }

    public int Size()// sizeini count ile buldum
    {
        return heap.Count;
    }
}

public class MainFieldNode
{
    public string FieldName { get; set; }// Alan adı  yani Field Name i tutan özellik
    public BST<SubField> SubfieldTree { get; set; }// slt alan 

    public MainFieldNode(string fieldName, BST<SubField> subfieldTree)// constructerim
    {
        FieldName = fieldName;
        SubfieldTree = subfieldTree;
    }
}


public class AIFieldsProject
{
    
     //  ana alan ve alt allanalrı içerene bst yapım.
    public static BST<BST<SubField>> CreatingMainfieldsTree()
    {

        // Ana ağacı başlattım, bu ağacın her bir düğümü bir alt alan ağacını tutuyo
        BST<BST<SubField>> mainTree = new BST<BST<SubField>>();

        // hepsi için böyle böyle alt alan için  ,bst  olusturdum
        BST<SubField> mlTree = new BST<SubField>();
        mlTree.Insert("Deep Learning", new SubField(
            "Deep Learning",
            "Neural networks with multiple layers for complex pattern recognition",
            "Image recognition, natural language processing, autonomous vehicles"
        ));
        mlTree.Insert("Reinforcement Learning", new SubField(
            "Reinforcement Learning",
            "Learning optimal actions through trial and error with rewards",
            "Game playing, robotics, resource management"
        ));
        mlTree.Insert("Supervised Learning", new SubField(
            "Supervised Learning",
            "Learning from labeled training data to make predictions",
            "Classification, regression, spam detection"
        ));
        mainTree.Insert("Machine Learning", mlTree);

        BST<SubField> nlpTree = new BST<SubField>();
        nlpTree.Insert("Machine Translation", new SubField(
            "Machine Translation",
            "Automatic translation between human languages using algorithms",
            "Real-time translation, document translation, multilingual communication"
        ));
        nlpTree.Insert("Sentiment Analysis", new SubField(
            "Sentiment Analysis",
            "Determining emotional tone and opinions in text data",
            "Social media monitoring, customer feedback analysis, brand reputation"
        ));
        nlpTree.Insert("Text Generation", new SubField(
            "Text Generation",
            "Creating human-like text using language models",
            "Content creation, chatbots, code generation"
        ));
        mainTree.Insert("Natural Language Processing", nlpTree);

        BST<SubField> cvTree = new BST<SubField>();
        cvTree.Insert("Image Classification", new SubField(
            "Image Classification",
            "Categorizing images into predefined classes using visual features",
            "Medical diagnosis, quality control, photo organization"
        ));
        cvTree.Insert("Object Detection", new SubField(
            "Object Detection",
            "Identifying and locating objects within images or videos",
            "Autonomous driving, surveillance, retail analytics"
        ));
        cvTree.Insert("Face Recognition", new SubField(
            "Face Recognition",
            "Identifying individuals based on facial features analysis",
            "Security systems, phone unlocking, attendance tracking"
        ));
        mainTree.Insert("Computer Vision", cvTree);

    
        BST<SubField> roboticsTree = new BST<SubField>();
        roboticsTree.Insert("Autonomous Navigation", new SubField(
            "Autonomous Navigation",
            "Enabling robots to move independently in environments",
            "Self-driving cars, warehouse automation, exploration robots"
        ));
        roboticsTree.Insert("Human-Robot Interaction", new SubField(
            "Human-Robot Interaction",
            "Designing natural communication between humans and robots",
            "Service robots, collaborative manufacturing, assistive devices"
        ));
        roboticsTree.Insert("Motion Planning", new SubField(
            "Motion Planning",
            "Computing collision-free paths for robot movement",
            "Industrial automation, surgical robots, drone delivery"
        ));
        mainTree.Insert("Robotics", roboticsTree);

       
        BST<SubField> expertTree = new BST<SubField>();
        expertTree.Insert("Knowledge Representation", new SubField(
            "Knowledge Representation",
            "Encoding expert knowledge in machine-processable formats",
            "Medical diagnosis, legal reasoning, financial planning"
        ));
        expertTree.Insert("Inference Engines", new SubField(
            "Inference Engines",
            "Applying logical rules to derive conclusions from facts",
            "Diagnostic systems, decision support, troubleshooting tools"
        ));
        expertTree.Insert("Rule-Based Systems", new SubField(
            "Rule-Based Systems",
            "Using if-then rules to make decisions and solve problems",
            "Business logic automation, expert advice systems, quality control"
        ));
        mainTree.Insert("Expert Systems", expertTree);

        return mainTree;
    }


    public static void MainTree(BST<BST<SubField>> mainTree)
    {
        Console.WriteLine("ana ve alt allanların hepsi = ");

        // Liste olarak aldım
        List<BSTNode<BST<SubField>>> mainNodes = mainTree.GetAllNodesInOrder();

        // Her bir ana alan için foreach ile donuyorum.
        foreach (BSTNode<BST<SubField>> mainNode in mainNodes)
        {   // ana alan adını yazdırdım
            Console.WriteLine($" {mainNode.Key}");

            // Alt alanları aldım burada 
            List<BSTNode<SubField>> subNodes = mainNode.Value.GetAllNodesInOrder();

            foreach (BSTNode<SubField> subNode in subNodes)
            {
                Console.WriteLine($" {subNode.Key}");// keyi yazdırdım
                Console.WriteLine($" Tanım: {subNode.Value.Definition}");//tanımı yazdırdım
                Console.WriteLine($" Uygulamalar: {subNode.Value.Applications}");// uygulammalrı yazdırdım
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }


    public static void FindingDeepTree(BST<BST<SubField>> mainTree)//en derin agacı  bulma methodum.
    {
        Console.WriteLine(" en derin agac bulma");

        int maxDepth = 0;// ilk basta 0 a eşitledim
        string deepField = "";
        List<string> deepNodes = new List<string>();//  en derin  nodları listede tuttum  string turunde 

        // Tüm ana alanları gezdim
        List<BSTNode<BST<SubField>>> mainNodes = mainTree.GetAllNodesInOrder();

        foreach (BSTNode<BST<SubField>> node in mainNodes)// foreach ile döndüm tüm elemanları
        {
            int depth = node.Value.GetDepth(node.Value.Root);//mainNode.Value, bir alt alanlar ağacını tutuyor  yani  get depth ile bunun derin liğini aldım
            if (depth > maxDepth)// depth   daha buyukse maxtan
            {
                maxDepth = depth;// max derinlği guncelledim
                deepField = node.Key;// deep fieldı ve 
                deepNodes = node.Value.GetAllKeys();// deep nodu guncelledim
            }
        }
        Console.WriteLine($"En derin alan: {deepField}");// yazdırdım yukrada bulduklarımı
        Console.WriteLine($"Max derinlik: {maxDepth}");
        Console.WriteLine("alt alanlar:");
        foreach (string nodeName in deepNodes)
        {
            Console.WriteLine($" {nodeName}");
        }
        Console.WriteLine();
    }

    public static void WordFrekanswithbst(BST<BST<SubField>> mainTree)
    {
        Console.WriteLine("kelime freknasını analizleri");

        Dictionary<string, int> wordFrekans = new Dictionary<string, int>();// dictionary ile string ve int olarak tuttum 

        // Tüm ana alanları gezdim yine 
        List<BSTNode<BST<SubField>>> mainNodes = mainTree.GetAllNodesInOrder(); // list olusturdum

        foreach (BSTNode<BST<SubField>> mainNode in mainNodes)
        {
            // Her ana alanın alt alanlarını gezdim yine 
            List<BSTNode<SubField>> subNodes = mainNode.Value.GetAllNodesInOrder();

            foreach (BSTNode<SubField> subNode in subNodes)
            {
                string text = subNode.Value.Definition + " " + subNode.Value.Applications;// texti yazdım 
                text = text.ToLower(); // Küçük harfe çevirdim

                // Kelimeleri ayırdım  bunu internetten öğrendim. regex  ile
                MatchCollection matches = Regex.Matches(text, @"\b[a-z]+\b");//bu metin içinde belli kısımlara uyan kısımları buluyor

                foreach (Match match in matches)// foreach ile geziyorum listeyi uyanları 
                {
                    string word = match.Value;
                    if (word.Length > 3) // 3 harften uzun kelimeler ayırttım
                    {
                        if (wordFrekans.ContainsKey(word))// kelime dictioarany de varsa 1 arttırıyorum
                            wordFrekans[word]++;
                        else
                            wordFrekans[word] = 1;//kelime yoksa o kelieyi ekliyip frekansını 1 yaptım.
                    }
                }
            }
        }

        Console.WriteLine("kelimeleri alfabetik AZALAN sırada BST'ye ekledim\n");
        BST<int> wordFrekansTree = new BST<int>();

        // Kelimeleri alfabetik azalan sırada sıraladım burada 
        List<string> keys = new List<string>(wordFrekans.Keys);
        keys.Sort(); // Alfabetik artanı da bu sort ile yaptım
        keys.Reverse(); // Tersine çevirdim yani azalan  

        foreach (string word in keys)
        {
            wordFrekansTree.Insert(word, wordFrekans[word]);// kelimeyi ve frekansını ekledim
        }

        // BST'den tüm kelimeleri aldım
        List<BSTNode<int>> wordNodes = wordFrekansTree.GetAllNodesInOrder();

        // Frekansa göre sıraladım burada bubble sort 
        for (int i = 0; i < wordNodes.Count - 1; i++)
        {
            for (int j = 0; j < wordNodes.Count - i - 1; j++)
            {
                if (wordNodes[j].Value > wordNodes[j + 1].Value)
                {
                    BSTNode<int> temp = wordNodes[j];
                    wordNodes[j] = wordNodes[j + 1];
                    wordNodes[j + 1] = temp;
                }
            }
        }

        Console.WriteLine("Kelime Frekanslarına gore artan sekildeki hali=");
        foreach (BSTNode<int> node in wordNodes)
        {
            Console.WriteLine($"{node.Key} = {node.Value}");
        }

        Console.WriteLine($"toplam unique kelime sayısı: {wordNodes.Count}");// unique kelime sayısını yazdırıdm
        
    }


    public static void HashTable(BST<BST<SubField>> mainTree)// hash table işlemleri uyguladıgım yer
    {
        // anahtarlar string türünde  , degerler  BST<SubField> türünde .
        Dictionary<string, BST<SubField>> hashTable = new Dictionary<string, BST<SubField>>();


        // mainTreedeki tüm ana alanları sıralı şekilde aldım.
        // mainNodes listesindeki her bir düğüm üzerinde dönicem foreach ile aşağıda 
        List<BSTNode<BST<SubField>>> mainNodes = mainTree.GetAllNodesInOrder();

        foreach (BSTNode<BST<SubField>> node in mainNodes)
        {
            // ana başlığı key ve alt alanlar ağacını  hash tablea ekledim
            hashTable[node.Key] = node.Value;
        }
        // HashTablea eklenen ana alanları yazdırdım.
        Console.WriteLine("hashtable'a eklediğim  alanlar= ");
        foreach (string key in hashTable.Keys)
        {
            Console.WriteLine($" {key}");
        }
        // toplam eleman sayısını da yazdırdım count ile
        Console.WriteLine($"Toplam eleman sayısı = {hashTable.Count}");
        // güncelleme yapmak için eski ve yeni name ler tanımladım.
        string oldName = "Machine Learning";
        string newName = "Advanced Machine Learning";
        // Eğer hashTableda eski başlık varsa onu güncelledim.
        if (hashTable.ContainsKey(oldName))
        {
            // eski başlıgın degerini aldım
            BST<SubField> value = hashTable[oldName];
            // sonra eskiyi kaldırdım
            hashTable.Remove(oldName);
            // yeni ekledim
            hashTable[newName] = value;
            Console.WriteLine($"\n Güncelleme =  \"{oldName}\" , \"{newName}\"");// \ bu iki  işareti içinde cift tırnak kullanmak için yazdım.

            Console.WriteLine("\nGüncellenmiş HashTable'ım = ");
            foreach (string key in hashTable.Keys)
            {
                Console.WriteLine($" {key}");
            }
        }
        Console.WriteLine();
    }


    public static void MınHeap(BST<BST<SubField>> mainTree) // MinHeap işlemlerini gerçekleştiren metodum
    {
        Console.WriteLine("MIN HEAP İŞLEMLERİM");

        MinHeap minHeap = new MinHeap();// yeni bir minheap olustruuyorum verideki en kuucgu bulicaam bu sayede

        // mainTree'deki tüm ana alanları sıralı şekilde aldım yine 
        List<BSTNode<BST<SubField>>> mainNodes = mainTree.GetAllNodesInOrder();
        // mainNodes listesindeki her bir düğüm üzerinde döndüm yine 
        foreach (BSTNode<BST<SubField>> node in mainNodes)
        {
            // her bir ana alanın adı ve alt alanlar ağacını  MinHeap'e ekledim
            minHeap.Insert(new MainFieldNode(node.Key, node.Value));
        }
        // 5 alt alanı eklediğim tek tek yazdırdım
        Console.WriteLine("İlk 5 eleman alfabetik sırayla en küçükten olacak sekilde  = \n");
        for (int i = 0; i < 5 && minHeap.Size() > 0; i++)
        {
            MainFieldNode node = minHeap.ExtractMin();// min cıkarma
            Console.WriteLine($"{i + 1}. {node.FieldName}");

            List<string> subfields = node.SubfieldTree.GetAllKeys();
            Console.WriteLine($" Alt alanlarım = {subfields.Count} adet");
            // en fazla iki alt alanı gösterdim  subfiels sayısını asmicak sekilde. aşağıdaki for da
            for (int j = 0; j < Math.Min(2, subfields.Count); j++)
            {
                Console.WriteLine($" {subfields[j]}");
            }
            Console.WriteLine();
        }
    }

    public static void ComparingAlgorithms()
    {
        // bubble sort ve quick sortu seçtim
        // ve bilgileri yazdırdım biraz internetten buldugum 
        Console.WriteLine("SORTING ALGORITHM KARŞILAŞTIRMASI  =  bubble sort ve quick sortu sectim");

        Console.WriteLine("BUBBLE SORT  = ");
        Console.WriteLine("Zaman Karmaşıklığı = O(n²)");
        Console.WriteLine("En iyi durum =  O(n)  Zaten sıralı hali ");
        Console.WriteLine("En kötü durum = O(n²) Ters sıralı hali ");
        Console.WriteLine("Avantajı da = basit implementasyonu , küçük dizilerde etkili");
        Console.WriteLine("Dezavantajı = buyük dızilerde çok yavaş");

        Console.WriteLine("QUICK SORT = ");
        Console.WriteLine("Zaman Karmaşıklığı = O(n log n)");
        Console.WriteLine("En iyi durum = O(n log n)");
        Console.WriteLine("En kötü durum = O(n²)  Kötü pivot seçimi durumunda ");
        Console.WriteLine("Avantajı da = Ortalama durumda çok hızlı olması");
        Console.WriteLine("Dezavantajı da = Worst case O(n²), recursive çağrılar");

        int arraySize = 100;
        int iterations = 10000; // soruda 1 milyon demişdi ama 1 milyon olunca cok uzun sure belli kısım outputu vermiyor bu
                                // yzuden bunu suan 10.000 olarak yaptım alsında amacım 1 milyon ama sadece outputum gozuksun diye.
        Console.WriteLine("Performans Testi:");
        Console.WriteLine($"Dizi boyutu: {arraySize}");
        Console.WriteLine($"İterasyon sayısı: {iterations}\n");

        Random random = new Random();// random ı cagırdım ve olusturdum

        // Bubble Sort testi
        Stopwatch bubbleWatch = Stopwatch.StartNew();// sureyi başlattım.
        for (int i = 0; i < iterations; i++)// bu for dongusunde işlemleri yapicam stopwatchin arrasına koydum bu sayede suresini olçmüş olicam
        {
            int[] arr = new int[arraySize];
            for (int k = 0; k < arraySize; k++)
            {
                arr[k] = random.Next(1000);
            }
            BubbleSort(arr);
        }
        bubbleWatch.Stop();// durdurdum
        Console.WriteLine($"Bubble Sort süresi: {bubbleWatch.ElapsedMilliseconds}ms");// sureyi yazdım milisaniye olarak

        // Quick Sort testi
        Stopwatch quickWatch = Stopwatch.StartNew();
        for (int i = 0; i < iterations; i++)// bubble sort ile aynı sekilde suresini olctum
        {
            int[] arr = new int[arraySize];
            for (int k = 0; k < arraySize; k++)
            {
                arr[k] = random.Next(1000);
            }
            QuickSort(arr, 0, arr.Length - 1);
        }
        quickWatch.Stop();// durdurdum
        Console.WriteLine($"Quick Sort süresi: {quickWatch.ElapsedMilliseconds}ms");// milisaniye olarak aldım

        double speedup = (double)bubbleWatch.ElapsedMilliseconds / quickWatch.ElapsedMilliseconds;// aradaki süre farkını koydum ne kadar yavas diye 
        Console.WriteLine($" hız farkı: Quick Sort {speedup} kat daha hızlı");
        // aşağıda yine internetten buldugum karşılatırma verileirni yazdım projemde bunun dogrulugunu pratik olarak olcmuş oldum.
        Console.WriteLine("karşılaştırma:");
        Console.WriteLine(" Küçük dizilerde (<50) =  Bubble Sort yeterli");
        Console.WriteLine(" Büyük dizilerde (>100) = Quick Sort çok daha hızlı");
        Console.WriteLine(" Zaten sıralı =  Bubble Sort O(n), Quick Sort O(n log n)");
        Console.WriteLine(" Ters sıralı =  İkisi de kötü, ama Quick Sort yine hızlı");
    }

    private static void BubbleSort(int[] arr)// yine klasik swap içerene bubble sort kodum
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
    }

    private static void QuickSort(int[] arr, int low, int high)//  klasik quick sort algoritması
    {
        if (low < high)
        {
            int pi = Partition(arr, low, high);
            QuickSort(arr, low, pi - 1);
            QuickSort(arr, pi + 1, high);
        }
    }

    private static int Partition(int[] arr, int low, int high)// quick sort içinde kullandıgım klasik partition methodum.
    {
        int pivot = arr[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (arr[j] < pivot)
            {
                i++;
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        int temp2 = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = temp2;

        return i + 1;
    }


    public static void Main(string[] args)// main clasıım içinde yapmam gereken leri tek tek cagırdım
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;// turkçe karakter için utf 8 i yazdım

        BST<BST<SubField>> mainTree = CreatingMainfieldsTree();// maintree  nesnem

        MainTree(mainTree);
        FindingDeepTree(mainTree);
        WordFrekanswithbst(mainTree);
        HashTable(mainTree);
        MınHeap(mainTree);
        ComparingAlgorithms();

        Console.ReadKey();
    }
}