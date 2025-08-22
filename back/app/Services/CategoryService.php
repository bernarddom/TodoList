<?php
namespace App\Services;

use App\Models\Category;
use Symfony\Component\Translation\Exception\NotFoundResourceException;

class CategoryService extends Service {
    public function store(
        $id,
        $name,
        $description,
    ) {
        $category = $id != null ? Category::find($id) : new Category();
        $category->name = $name;
        $category->description = $description;
        return $category->save();
    }

    public function getById($id){
        $category = Category::find($id);
        if($category == null) {
            throw new NotFoundResourceException("id");
        }
        return $category;
    }

    public function getCategories() {
        $category = new Category();
        return $category->all();
    }
}