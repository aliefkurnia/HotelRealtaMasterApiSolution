proses bisnis untuk membuat PO baru jika vendor berbeda:

1. User membuat pesanan pembelian untuk produk tertentu dari vendor A dan mengisi detail pesanan seperti jumlah, harga, dll. Kemudian, user menambahkan pesanan ke dalam tabel "purchase_order_header" dan "purchase_order_detail" menggunakan perintah INSERT INTO.
2. Kemudian, vendor A memberikan confermation dan PO tersebut statusnya diubah menjadi "approved".
3. Beberapa saat kemudian, user memutuskan untuk membeli produk tambahan dari vendor B dan ingin membuat PO baru.
4. User memutuskan untuk membuat PO baru untuk vendor B karena produk yang dibeli berbeda dari produk sebelumnya. User mengisi detail pesanan seperti jumlah, harga, dll. Kemudian, user menambahkan pesanan ke dalam tabel "purchase_order_header" dan "purchase_order_detail" menggunakan perintah INSERT INTO.
5. Kemudian, vendor B memberikan confirmation dan PO tersebut statusnya diubah menjadi "approved".
6. Setelah PO dari kedua vendor disetujui, user dapat mengikuti proses pengiriman dan pembayaran seperti biasa.

Dalam hal ini, user harus membuat PO baru untuk vendor B karena produk yang dibeli berbeda dari produk yang dibeli dari vendor A. Membuat PO baru memungkinkan user untuk memisahkan pesanan untuk vendor yang berbeda dan memprosesnya secara terpisah.