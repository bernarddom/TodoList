<?php

use App\Models\Category;
use App\Models\TodoList;

class CategoryService extends Service {
    public function store(
        $name,
        $description,
        $userId
    ) {
        $todoList = new Category();
        $todoList->name = $name;
        $todoList->name = $description;
        return $todoList->save();
    }

    public function getLists() {
        $todoList = new TodoList();
        return $todoList->all();
    }
}