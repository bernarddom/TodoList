<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class TodoList extends Model
{
    private $name;
    private $description;
    private $userId;

    protected $table = "todo_lists";
}
