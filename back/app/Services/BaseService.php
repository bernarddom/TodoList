<?php
namespace App\Services;

use App\Models\Category;
use Illuminate\Database\Eloquent\Model;
use Symfony\Component\Translation\Exception\NotFoundResourceException;

class BaseService extends Service {
    protected Model $model;
    public function store(
        $data
    ) {
        $model = $data->id != null ? $this->model::find($id) : $this->model;
        $model = $data;
        $model->description = $description;
        return $model->save();
    }

    public function getById($id){
        $category = Category::find($id);
        if($category == null) {
            throw new NotFoundResourceException("Category with id ".$id." does not exist");
        }
        return $category;
    }

    public function getCategories() {
        $category = new Category();
        return $category->all();
    }
}