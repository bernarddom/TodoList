<?php
namespace App\Services;

use App\Models\Category;

class CategoryService extends Service {
    public function store(
        $name,
        $description,
    ) {
        $category = new Category();
        $category->name = $name;
        $category->name = $description;
        return $category->save();
    }

    public function getCategories() {
        $category = new Category();
        return $category->all();
    }
}