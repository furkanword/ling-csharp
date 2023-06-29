using System.Linq;

public class LinqQueries{
    List<Book> lstBooks = new List<Book>();
    public LinqQueries(){
        using(StreamReader reader = new StreamReader("books.json")){
            string json = reader.ReadToEnd();
            this.lstBooks = System.Text.Json.JsonSerializer.Deserialize<List<Book>>(json,new System.Text.Json.JsonSerializerOptions(){PropertyNameCaseInsensitive = true}) ?? new List<Book>();
        }
    }
    public IEnumerable<Book> AllCollection(){
        return lstBooks;
    }
    /// <summary>
    ///Metodo que permite seleccionar los libros cuyo año de pub mayor 2000
    /// </summary>
    /// <returns>Coleccion de libros que me permiten hacer la busqueda</returns>
    public IEnumerable<Book> LibrosDespues2000(){
        //Extension method
        //return lstBooks.Where(book => book.PublishedDate.Year > 2000);
        return from book in lstBooks where book.PublishedDate.Year >200 select book;
    }
    /// <summary>
    /// devuelve una colección que contiene aquellos libros 
    /// que tienen más de 250 páginas y cuyos títulos contienen
    ///  la subcadena "in Action". Es una forma de filtrar y obtener
    ///  una lista específica de libros basada en ciertos criterios.
    /// </summary>
    /// <returns>Busqueda y filtro de libros con mas de 250 paginas</returns>
    public IEnumerable<Book> LibrosMas250Pag(){
        return from book in lstBooks 
                where book.PageCount > 250 
                && (book.Title ?? String.Empty).Contains("in Action") 
                select book;
    }
    /// <summary>
    /// ListarBookConStatus() toma una lista de libros (lstBooks) 
    /// y devuelve una colección que contiene solo aquellos
    /// libros que tienen un estado asignado.
    /// </summary>
    /// <returns>devuelve una colección de libros (IEnumerable<Book>)
    ///  que cumplen con cierta condición.</returns>
    public IEnumerable<Book> ListarBookConStatus(){
        return from book in lstBooks
            where book.Status != String.Empty
            select book;
    }
    /// <summary>
    /// Puede utilizarse para verificar si todos los libros
    /// cumplen con cierto criterio de validación, en este 
    /// caso, que tengan un estado no vacío
    /// </summary>
    /// <returns>devuelve true si todos los libros de 
    /// la lista tienen un estado asignado </returns>
     
    public bool ValidarStatus(){
        return lstBooks.All(book => book.Status != String.Empty);
    }
    /// <summary>
    /// Puede utilizarse para verificar si existe un libro
    ///  que cumpla con cierto criterio de validación, en este
    ///  caso, si hay un libro publicado en un año específico.
    /// </summary>
    /// <returns>realiza una validación en la lista de libros</returns>
    
    public bool ValidarFechaPub(){
        return lstBooks.Any(book => book.PublishedDate.Year == 2005);
    }
    /// <summary>
    /// devuelve una colección que contiene aquellos libros 
    /// cuyas categorías incluyen "Python". Es una forma 
    /// de filtrar y obtener una lista específica de libros 
    /// basada en su categoría
    /// </summary>
    /// <returns>Obtiene la lista espesifica basada en caracteristicas</returns>
    public IEnumerable<Book> GetBooksPython(){
        return lstBooks.Where(
            book => (book.Categories ?? Array.Empty<String>())
            .Contains("Python"));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Book> GetBooksSortAsc(){
        return lstBooks.Where(
            book => (book.Categories ?? Array.Empty<string>())
            .Contains("Java"))
            .OrderBy(book => book.Title);
       
    }
    
    public IEnumerable<Book> GetBooksSortDsc(){
        return lstBooks.Where(
            book => book.PageCount>450)
            .OrderByDescending(book => book.PageCount);
    }

    public IEnumerable<Book> GetBooksTake(){
        return lstBooks
        .Where(book => (book.Categories ?? Array.Empty<string>())
        .Contains("Java"))
        .OrderByDescending(book => book.PublishedDate).Take(3);
    
    }
    //Muestra los ultimos tres libro
    public IEnumerable<Book> GetBooksTakeLast(){
        return lstBooks
        .Where(book => (book.Categories ?? Array.Empty<string>())
        .Contains("Java"))
        .OrderBy(book => book.PublishedDate).TakeLast(3);
    
    }
    public IEnumerable<Book> GetBooksSkipTerceyCuarto(){
        return lstBooks
        .Where(book => book.PageCount > 400)
        .Take(4)
        .Skip(2);
    }
    public IEnumerable<Book> GetBooksSelect(){
        return lstBooks.Take(3)
        .Select(book => new Book{ Title = book.Title,PageCount =book.PageCount });
    }
    public int GetBooksCount(){
        return lstBooks
        .Count(book => book.PageCount>=200 && book.PageCount<=500);
    }
    public long GetBooksLongCount(){
        return lstBooks
        .LongCount(book => book.PageCount>=200 && book.PageCount<=500);
    }
    public DateTime GetBooksDatePublishMinor(){
        return lstBooks.Min(book => book.PublishedDate);
    }
    public DateTime GetBooksDatePublishMax(){
        return lstBooks.Max(book => book.PublishedDate);
    }
    public IEnumerable<Book> GetBooksListDatePublishMinor(){
        var fechaPublicacionMinima = lstBooks.Min(libro => libro.PublishedDate);
        return lstBooks.Where(libro => libro.PublishedDate == fechaPublicacionMinima);
    }

}
